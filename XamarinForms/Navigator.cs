using Blue.MVVM.IoC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Blue.MVVM.Navigation {
    public partial class Navigator {

        public Navigator(IViewLocator viewLocator, ITypeResolver typeResolver, NavigationPage navigationRoot) 
            : this (viewLocator, typeResolver) {

            if (navigationRoot == null)
                throw new ArgumentNullException(nameof(navigationRoot), "must not be null");
            _NavigationRoot = navigationRoot;
        }

        private readonly NavigationPage _NavigationRoot;

        public async Task PopAsync() {
            await _NavigationRoot.PopAsync();
        }

        private async Task<bool> PushAsyncCore<TViewModel>(TViewModel viewModel, Func<TViewModel, Task> asyncConfig = null) {
            var viewType = await _ViewLocator.ResolveViewTypeForAsync<TViewModel>();
            var page = _TypeResolver.Resolve<Page>(viewType);

            SetViewModel<TViewModel, BindableObject>(page, viewModel, (v, vm) => v.BindingContext = vm);

            if (asyncConfig != null)
                await asyncConfig(viewModel);

            await _NavigationRoot.PushAsync(page);

            return true;
        }
    }
}
