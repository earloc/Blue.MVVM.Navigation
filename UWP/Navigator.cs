﻿using Blue.MVVM.IoC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Blue.MVVM.Navigation {
    public partial class Navigator {
        public Navigator(IViewLocator viewLocator, ITypeResolver typeResolver, Frame navigationRoot) 
            : this(viewLocator, typeResolver) {
           
            if (navigationRoot == null)
                throw new ArgumentNullException(nameof(navigationRoot), "must not be null");
            _NavigationRoot = navigationRoot;
        }

        private readonly Frame _NavigationRoot;

        public async Task PopAsync() {
            await Task.Yield();

            if (_NavigationRoot.CanGoBack)
                _NavigationRoot.GoBack();
        }

        private async Task<bool> PushAsyncCore<TViewModel>(TViewModel viewModel, Func<TViewModel, Task> asyncConfig = null) {
            var viewType = await _ViewLocator.ResolveViewTypeForAsync<TViewModel>();

            bool success = _NavigationRoot.Navigate(viewType);
            if (!success)
                return false;

            if (asyncConfig != null) {
                await asyncConfig(viewModel);
            }

            var view = _NavigationRoot.Content as FrameworkElement;

            SetViewModel<TViewModel, FrameworkElement>(view, viewModel, (v, vm) => v.DataContext = vm);

            return true;
        }
    }
}