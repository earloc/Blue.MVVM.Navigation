using Blue.MVVM.IoC;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

#if WPF
using System.Windows;
using System.Windows.Controls;
#else
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

#endif
namespace Blue.MVVM.Navigation {
    partial class ModalNavigator {

        private async Task<bool?> ShowModalCoreAsync<TViewModel>(TViewModel viewModel, Func<TViewModel, Task> asyncConfig = null) {
            var viewType = await ViewLocator.ResolveViewTypeForAsync<TViewModel>(true);

            if (asyncConfig != null) {
                await asyncConfig(viewModel);
            }

            var view = await ViewLocator.ResolveViewTypeForAsync<TViewModel>();

            SetViewModel<TViewModel, FrameworkElement>(view, viewModel, (v, vm) => v.DataContext = vm);

#if WPF
            var dialog = new Window();
            var result = dialog.ShowDialog();
            return result;
#else
            var dialog = new ContentDialog();
            var result = await dialog.ShowAsync();
            return ToBool(result);

#endif
        }

#if WINDOWS_UWP

        private bool? ToBool(ContentDialogResult result) {
            if (result == ContentDialogResult.Primary)
                return true;
            if (result == ContentDialogResult.Secondary)
                return false;

            return null;

        }

#endif

    }
}
