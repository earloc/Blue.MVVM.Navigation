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

        public async Task ShowModalAsync<TViewModel>(bool? animated = null) {
            var viewModel = TypeResolver.Resolve<TViewModel>();
            await ShowModalCoreAsync(viewModel, animated: animated);
        }

        public async Task ShowModalAsync<TViewModel>(Action<TViewModel> config = null, bool? animated = null) {
            var viewModel = TypeResolver.Resolve<TViewModel>();
            await ShowModalCoreAsync(viewModel, async vm => {
                await CrossTask.Yield();
                config?.Invoke(vm);
            }, animated);
        }

        public async Task ShowModalAsync<TViewModel>(Func<TViewModel, Task> asyncConfig = null, bool? animated = null) {
            var viewModel = TypeResolver.Resolve<TViewModel>();
            await ShowModalCoreAsync(viewModel, asyncConfig, animated);
        }

        public async Task ShowModalAsync<TViewModel>(TViewModel viewModel, bool? animated = null) {
            await ShowModalCoreAsync(viewModel, animated: animated);
        }
    }
}
