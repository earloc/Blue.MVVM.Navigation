using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blue.MVVM.Navigation
{
    public static class INavigatorExtensions {
        public static async Task<bool> TryPushAsync<TViewModel>(this INavigator source, Action<TViewModel> viewModelConfig = null) {
            if (source == null)
                return false;
            return await source.PushAsync(viewModelConfig);
        }

        public static async Task<bool> TryPushAsync<TViewModel>(this INavigator source, Func<TViewModel, Task> asyncViewModelConfig = null) {
            if (source == null)
                return false;
            return await source.PushAsync(asyncViewModelConfig);
        }

        public static async Task<bool> TryPushAsync<TViewModel>(this INavigator source, TViewModel viewModel) {
            if (source == null)
                return false;
            return await source.PushAsync(viewModel);
        }
        public static async Task TryPopAsync<TViewModel>(this INavigator source) {
            if (source == null)
                return;
            await source.PopAsync();
        }
    }
}
