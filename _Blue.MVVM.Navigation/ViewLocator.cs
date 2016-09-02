using Blue.MVVM.IoC;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Blue.MVVM.Extensions;
using System.Reflection;

namespace Blue.MVVM.Navigation {
    public class ViewLocator : IViewLocator {

        public ViewLocator(ITypeResolver resolver) {
            if (resolver == null)
                throw new ArgumentNullException(nameof(resolver), "must not be null");

            _TypeResolver = resolver;
        }

        private readonly ITypeResolver _TypeResolver;

        public abstract Task<Type> ResolveViewTypeForAsync<TViewModel>();

        public async Task<TView> ResolveViewForAsync<TViewModel, TView>(Action<TViewModel> viewModelConfiguration) {
            var view = await CreateViewAsync<TViewModel, TView>();
            if (view != null) {
                var viewModel = await _TypeResolver.ResolveAsync(viewModelConfiguration);
                await InitializeViewAsync(viewModel, view);
            }

            return view;
        }

        public async Task<TView> ResolveViewForAsync<TViewModel, TView>(TViewModel viewModel) {
            var view = await CreateViewAsync<TViewModel, TView>();
            if (view != null)
                await InitializeViewAsync(viewModel, view);
            return view;
        }

        private async Task<TView> CreateViewAsync<TViewModel, TView>() {
            var viewType = await ResolveViewTypeForAsync<TViewModel>();
            if (viewType == null)
                return default(TView);

            return _TypeResolver.Resolve<TView>(viewType);
        }

        protected async Task InitializeViewAsync<TViewModel, TView>(TViewModel viewModel, TView view) {
            var viewModelView = view as IView<TViewModel>;

            if (viewModelView != null) {
                viewModelView.ViewModel = viewModel;
                return;
            }

            if (TrySetDataContext(view, viewModel))
                return;
            if (TrySetBindingContext(view, viewModel))
                return;

            throw new Exception($"View initialization failed. View of type '{typeof(TView).FullName}' does not implement IView<TViewModel> and does not have a WPF/UWP/SL XAML style, public settable DataContext-property, nor a Xamarin.Forms XAML style, public settable BindingContext-property. Either ensure that one of these properties exist or implement IView<TViewModel>.");
        }

        private bool TrySetDataContext<TView, TViewModel>(TView view, TViewModel viewModel) {
            dynamic xamlView = view;
            try {
                xamlView.DataContext = viewModel;
                return true;
            }
            catch (TargetInvocationException) {
                return false;
            }
        }

        private bool TrySetBindingContext<TView, TViewModel>(TView view, TViewModel viewModel) {
            dynamic xamlView = view;
            try {
                xamlView.BindingContext = viewModel;
                return true;
            }
            catch (TargetInvocationException) {
                return false;
            }
        }
    }
}
