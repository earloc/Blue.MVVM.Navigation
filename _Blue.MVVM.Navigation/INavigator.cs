using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blue.MVVM.Navigation {
    public interface INavigator {
        Task<bool> PushAsync<TViewModel>(Func<TViewModel, Task> asyncConfig);
        Task<bool> PushAsync<TViewModel>(Action<TViewModel> config);
        Task<bool> PushAsync<TViewModel>(TViewModel viewModel);
        Task<bool> PushAsync<TViewModel>();
        Task PopAsync();
    }

    public static class INavigatorExtensions {
        public static async Task<bool> TryPushAsync<TViewModel>(this INavigator source, Action<TViewModel> viewModelConfig = null) {
            if (source == null)
                return false;
            return await source.PushAsync(viewModelConfig);
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
