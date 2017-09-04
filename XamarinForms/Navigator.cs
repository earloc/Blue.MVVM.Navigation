using Blue.MVVM.IoC;
using Blue.MVVM.Navigation.ViewLocators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Blue.MVVM.Navigation {
    public partial class Navigator {

        public Navigator(IViewLocator viewLocator, IServiceLocator serviceLocator, INavigation navigationRoot) 
            : this (viewLocator, serviceLocator) {
            System.Reflection.Assembly asm;
            
            if (navigationRoot == null)
                throw new ArgumentNullException(nameof(navigationRoot), "must not be null");
            _NavigationRoot = navigationRoot;
        }

        private readonly INavigation _NavigationRoot;

        public async Task PopAsync() {
            await _NavigationRoot.PopAsync();
        }

        private async Task<bool> PushCoreAsync<TViewModel>(TViewModel viewModel, Func<TViewModel, Task> asyncConfig = null) {
            var viewType = await ViewLocator.ResolveViewTypeForAsync(viewModel?.GetType() ?? typeof(TViewModel), true);
            var page = ServiceLocator.GetAs<Page>(viewType);

            SetViewModel<TViewModel, BindableObject>(page, viewModel, (v, vm) => v.BindingContext = vm);

            if (asyncConfig != null)
                await asyncConfig(viewModel);

            await _NavigationRoot.PushAsync(page);

            return true;
        }
    }
}
