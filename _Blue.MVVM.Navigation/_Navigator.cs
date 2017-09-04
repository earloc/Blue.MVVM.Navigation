using Blue.MVVM.IoC;
using Blue.MVVM.Navigation.ViewLocators;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Blue.MVVM.Navigation {
    partial class Navigator : NavigatorBase, INavigator {

        protected Navigator(IViewLocator viewLocator, IServiceLocator serviceLocator) 
            : base (viewLocator, serviceLocator) {
        }

        public async Task<bool> PushAsync<TViewModel>() where TViewModel : class {
            var viewModel = ServiceLocator.Get<TViewModel>();
            return await PushCoreAsync(viewModel);
        }

        public async Task<bool> PushAsync<TViewModel>(Action<TViewModel> config = null) where TViewModel : class {
            var viewModel = ServiceLocator.Get<TViewModel>();
            return await PushCoreAsync(viewModel, async vm => {
                await CrossTask.Yield();
                config?.Invoke(vm);
            });
        }

        public async Task<bool> PushAsync<TViewModel>(Func<TViewModel, Task> asyncConfig = null) where TViewModel : class {
            var viewModel = ServiceLocator.Get<TViewModel>();
            return await PushCoreAsync(viewModel, asyncConfig);
        }

        public async Task<bool> PushAsync<TViewModel>(TViewModel viewModel) where TViewModel : class {
            return await PushCoreAsync(viewModel);
        }

    }
}
