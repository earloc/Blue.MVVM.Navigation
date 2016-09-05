using Blue.MVVM.IoC;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Blue.MVVM.Navigation {
    partial class Navigator : INavigator {

        protected Navigator(IViewLocator viewLocator, ITypeResolver typeResolver) {
            if (viewLocator == null)
                throw new ArgumentNullException(nameof(viewLocator), "must not be null");
            _ViewLocator = viewLocator;

            if (typeResolver == null)
                throw new ArgumentNullException(nameof(typeResolver), "must not be null");
            _TypeResolver = typeResolver;
        }

        private readonly IViewLocator _ViewLocator;
        private readonly ITypeResolver _TypeResolver;

        public async Task<bool> PushAsync<TViewModel>() {
            var viewModel = _TypeResolver.Resolve<TViewModel>();
            return await PushAsyncCore(viewModel);
        }

        public async Task<bool> PushAsync<TViewModel>(Action<TViewModel> config = null) {
            var viewModel = _TypeResolver.Resolve<TViewModel>();
            return await PushAsyncCore(viewModel, async vm => {
                await CrossTask.Yield();
                config?.Invoke(vm);
            });
        }

        public async Task<bool> PushAsync<TViewModel>(Func<TViewModel, Task> asyncConfig = null) {
            var viewModel = _TypeResolver.Resolve<TViewModel>();
            return await PushAsyncCore(viewModel, asyncConfig);
        }

        public async Task<bool> PushAsync<TViewModel>(TViewModel viewModel) {
            return await PushAsyncCore(viewModel);
        }

        private void SetViewModel<TViewModel, TPlatformView>(object view, TViewModel viewModel, Action<TPlatformView, TViewModel> bind) where TPlatformView : class{
            if (SetViewModelOn(view as IView<TViewModel>, viewModel))
                return;

            var platformView = view as TPlatformView;
            if (platformView == null)
                throw new Exception($"View does not implement '{typeof(IView<TViewModel>).FullName}' nor inherits from platform specific view of type '{typeof(TPlatformView).FullName}'");

            bind(platformView, viewModel);
        }

        private bool SetViewModelOn<TViewModel>(IView<TViewModel> view, TViewModel viewModel) {
            if (view == null)
                return false;

            view.ViewModel = viewModel;
            return true;
        }

    }
}
