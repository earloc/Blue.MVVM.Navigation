using Blue.MVVM.IoC;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Blue.MVVM.Extensions;

namespace Blue.MVVM.Navigation {
    public abstract class ViewLocator : IViewLocator {

        public ViewLocator(ITypeResolver resolver) {
            if (resolver == null)
                throw new ArgumentNullException(nameof(resolver), "must not be null");

            _TypeResolver = resolver;
        }

        private readonly ITypeResolver _TypeResolver;

        protected internal abstract Task<Type> ResolveViewForAsync<TViewModel>();

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
            var viewType = await ResolveViewForAsync<TViewModel>();
            if (viewType == null)
                return default(TView);

            return _TypeResolver.Resolve<TView>(viewType);
        }

        protected abstract Task InitializeViewAsync<TViewModel, TView>(TViewModel viewModel, TView view);


    }
}
