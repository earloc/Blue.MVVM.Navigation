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
        //public async Task PushAsync<T>(T viewModel) {
        //    var page = await _ViewLocator.ResolveViewForAsync<T, Page>(viewModel);
        //    await _NavigationRoot.PushAsync(page);
        //}

        //public async Task PushAsync<T>(Action<T> viewModelConfig) {
        //    var viewType = await _ViewLocator.ResolveViewTypeForAsync<T, Page>();

        //    await _NavigationRoot.PushAsync(page);
        //}

        //private async Task<TView> ResolveViewForAsync<TViewModel, TView>(Action<TViewModel> viewModelConfiguration) {
        //    var view = await CreateViewAsync<TViewModel, TView>();
        //    if (view != null) {
        //        var viewModel = await _TypeResolver.ResolveAsync(viewModelConfiguration);
        //        await InitializeViewAsync(viewModel, view);
        //    }

        //    return view;
        //}

        //private async Task<TView> CreateViewAsync<TViewModel, TView>() {
        //    var viewType = await ResolveViewTypeForAsync<TViewModel>();
        //    if (viewType == null)
        //        return default(TView);

        //    return _TypeResolver.Resolve<TView>(viewType);
        //}
    }
}
