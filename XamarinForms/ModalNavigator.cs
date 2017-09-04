using Blue.MVVM.IoC;
using Blue.MVVM.Navigation.ViewLocators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Blue.MVVM.Navigation {
    public partial class ModalNavigator {
        
        public ModalNavigator(IViewLocator viewLocator, IServiceLocator serviceLocator, INavigation navigationRoot) 
            : base (viewLocator, serviceLocator) {
            if (navigationRoot == null)
                throw new ArgumentNullException(nameof(navigationRoot), "must not be null");
            _NavigationRoot = navigationRoot;
        }

        private readonly INavigation _NavigationRoot;
        private async Task ShowModalCoreAsync<TViewModel>(TViewModel viewModel, Func<TViewModel, Task> asyncConfig = null, bool? animated = null) {
            var viewType = await ViewLocator.ResolveViewTypeForAsync(viewModel?.GetType() ?? typeof(TViewModel), true);
            var page = ServiceLocator.GetAs<Page>(viewType);

            SetViewModel<TViewModel, BindableObject>(page, viewModel, (v, vm) => v.BindingContext = vm);

            if (asyncConfig != null)
                await asyncConfig(viewModel);


            var view = page as INavigationAwareView;

            await view.TryAppearingAsync();
            await _NavigationRoot.PushModalAsync(page, animated ?? DefaultSettings.IsAnimationEnabled);
            await view.TryAppearedAsync();
        }

        public async Task PopModalAsync(bool? animated = null) {
            var view = _NavigationRoot.ModalStack.Last() as INavigationAwareView;

            await view.TryDisappearingAsync();
            await _NavigationRoot.PopModalAsync(animated ?? DefaultSettings.IsAnimationEnabled);
            await view.TryDisappearedAsync();
        }



    }
}
