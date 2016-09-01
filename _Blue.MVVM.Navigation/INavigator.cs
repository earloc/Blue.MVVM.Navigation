using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blue.MVVM.Navigation {
    public interface INavigator {
        Task PushAsync<T> (Action<T> viewModelConfig);
        Task PushAsync<T>(T viewModel);
        Task PopAsync();
    }

    public static class INavigatorExtensions {
        public static async Task TryPushAsync<T>(this INavigator source, Action<T> viewModelConfig = null) {
            if (source == null)
                return;
            await source.PushAsync(viewModelConfig);
        }

        public static async Task TryPopAsync<T>(this INavigator source) {
            if (source == null)
                return;
            await source.PopAsync();
        }
    }
}
