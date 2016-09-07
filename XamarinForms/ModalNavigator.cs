using Blue.MVVM.IoC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Blue.MVVM.Navigation {
    public partial class ModalNavigator {
        
        public class Settings {
            public bool IsAnimationEnabled { get; set; }
        }

        public Settings Defaults { get; } = new Settings();

        public ModalNavigator(IViewLocator viewLocator, ITypeResolver typeResolver, INavigation navigationRoot) 
            : base (viewLocator, typeResolver) {

            if (navigationRoot == null)
                throw new ArgumentNullException(nameof(navigationRoot), "must not be null");
            _NavigationRoot = navigationRoot;
        }

        private readonly INavigation _NavigationRoot;
        private async Task ShowModalCoreAsync<TViewModel>(TViewModel viewModel, Func<TViewModel, Task> asyncConfig = null, bool? animated = null) {
            var viewType = await ViewLocator.ResolveViewTypeForAsync(viewModel?.GetType() ?? typeof(TViewModel), true);
            var page = TypeResolver.ResolveAs<Page>(viewType);

            SetViewModel<TViewModel, BindableObject>(page, viewModel, (v, vm) => v.BindingContext = vm);

            if (asyncConfig != null)
                await asyncConfig(viewModel);

            await _NavigationRoot.PushModalAsync(page, animated ?? Defaults.IsAnimationEnabled);
        }

        public async Task PopModalAsync(bool? animated = null) {
            await _NavigationRoot.PopModalAsync(animated ?? Defaults.IsAnimationEnabled);
        }
    }
}
