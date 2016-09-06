using Blue.MVVM.IoC;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blue.MVVM.Navigation {
    partial class ModalNavigator : NavigatorBase, IModalNavigator {

        protected ModalNavigator(IViewLocator viewLocator, ITypeResolver typeResolver)
            : base(viewLocator, typeResolver) {
        }

        public async Task<bool?> ShowModalAsync<TViewModel>() {
            var viewModel = TypeResolver.Resolve<TViewModel>();
            return await ShowModalCoreAsync(viewModel);
        }

        public async Task<bool?> ShowModalAsync<TViewModel>(Action<TViewModel> config = null) {
            var viewModel = TypeResolver.Resolve<TViewModel>();
            return await ShowModalCoreAsync(viewModel, async vm => {
                await CrossTask.Yield();
                config?.Invoke(vm);
            });
        }

        public async Task<bool?> ShowModalAsync<TViewModel>(Func<TViewModel, Task> asyncConfig = null) {
            var viewModel = TypeResolver.Resolve<TViewModel>();
            return await ShowModalCoreAsync(viewModel, asyncConfig);
        }

        public async Task<bool?> ShowModalAsync<TViewModel>(TViewModel viewModel) {
            return await ShowModalCoreAsync(viewModel);
        }
    }
}
