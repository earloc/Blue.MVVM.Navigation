﻿using Blue.MVVM.IoC;
using Blue.MVVM.Navigation.ViewLocators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


#if WPF
using System.Windows;
using System.Windows.Controls;
#else
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

#endif

namespace Blue.MVVM.Navigation {
    public partial class Navigator {
        public Navigator(IViewLocator viewLocator, IServiceLocator serviceLocator, Frame navigationRoot) 
            : this(viewLocator, serviceLocator) {
           
            if (navigationRoot == null)
                throw new ArgumentNullException(nameof(navigationRoot), "must not be null");
            _NavigationRoot = navigationRoot;
        }

        private readonly Frame _NavigationRoot;

        public async Task PopAsync() {
            await CrossTask.Yield();

            if (_NavigationRoot.CanGoBack)
                _NavigationRoot.GoBack();
        }

        private async Task<bool> PushCoreAsync<TViewModel>(TViewModel viewModel, Func<TViewModel, Task> asyncConfig = null) {
            var viewType = await ViewLocator.ResolveViewTypeForAsync(viewModel?.GetType() ?? typeof(TViewModel), true);

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
