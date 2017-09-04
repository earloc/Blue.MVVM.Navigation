using Blue.MVVM.IoC;
using Blue.MVVM.Navigation.ViewLocators;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blue.MVVM.Navigation {

    public partial class NavigatorBase {
        protected NavigatorBase(IViewLocator viewLocator, IServiceLocator serviceLocator) {
            ViewLocator = viewLocator ?? throw new ArgumentNullException(nameof(viewLocator), "must not be null");
            ServiceLocator = serviceLocator ?? throw new ArgumentNullException(nameof(serviceLocator), "must not be null");
        }

        protected IViewLocator ViewLocator { get; }
        protected IServiceLocator ServiceLocator { get; }

        protected void SetViewModel<TViewModel, TPlatformView>(object view, TViewModel viewModel, Action<TPlatformView, TViewModel> bind) where TPlatformView : class {
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
