using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blue.MVVM.Navigation {
    public static class IModalNavigatorExtensions {
        public static async Task TrShowModalAsync<TViewModel>(this IModalNavigator source, Action<TViewModel> viewModelConfig = null) {
            if (source == null)
                return;
            await source.ShowModalAsync(viewModelConfig);
        }

        public static async Task TryShowModalAsync<TViewModel>(this IModalNavigator source, Func<TViewModel, Task> asyncViewModelConfig = null) {
            if (source == null)
                return;
            await source.ShowModalAsync(asyncViewModelConfig);
        }

        public static async Task TryShowModalAsync<TViewModel>(this IModalNavigator source, TViewModel viewModel) {
            if (source == null)
                return;
            await source.ShowModalAsync(viewModel);
        }

        public static async Task TryPopModalAsync(this IModalNavigator source) {
            if (source == null)
                return;
            await source.PopModalAsync();
        }
    }
}
