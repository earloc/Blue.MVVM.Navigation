using Blue.MVVM.IoC;
using System;
using System.Collections.Generic;
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
    public partial class ModalNavigator {

        private async Task ShowModalCoreAsync<TViewModel>(TViewModel viewModel, Func<TViewModel, Task> asyncConfig = null, bool? animated = null) {
            var viewType = await ViewLocator.ResolveViewTypeForAsync(viewModel?.GetType() ?? typeof(TViewModel), true);

            if (asyncConfig != null) {
                await asyncConfig(viewModel);
            }

            var view = TypeResolver.ResolveAs<FrameworkElement>(viewType);

            SetViewModel<TViewModel, FrameworkElement>(view, viewModel, (v, vm) => v.DataContext = vm);

#if WPF
            var dialog = new Window();
            var result = dialog.ShowDialog();
#else
            var dialog = new ContentDialog();
            var result = await dialog.ShowAsync();

#endif
        }

        public async Task PopModalAsync(bool? animated = null) {
            await CrossTask.Yield();
        }

    }
}
