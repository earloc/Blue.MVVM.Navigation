using Blue.MVVM.IoC;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blue.MVVM.Navigation {

    public partial class NavigatorBase {
        protected NavigatorBase(IViewLocator viewLocator, ITypeResolver typeResolver) {
            if (viewLocator == null)
                throw new ArgumentNullException(nameof(viewLocator), "must not be null");
            ViewLocator = viewLocator;

            if (typeResolver == null)
                throw new ArgumentNullException(nameof(typeResolver), "must not be null");
            TypeResolver = typeResolver;
        }

        protected IViewLocator ViewLocator { get; }
        protected ITypeResolver TypeResolver { get; }

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
