using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blue.MVVM.Navigation {
    public static class IModalNavigatorExtensions {
        public static async Task TrShowModalAsync<TViewModel>(this IModalNavigator source, Action<TViewModel> viewModelConfig = null, bool? animated = null) {
            if (source == null)
                return;
            await source.ShowModalAsync(viewModelConfig, animated);
        }

        public static async Task TryShowModalAsync<TViewModel>(this IModalNavigator source, Func<TViewModel, Task> asyncViewModelConfig = null, bool? animated = null) {
            if (source == null)
                return;
            await source.ShowModalAsync(asyncViewModelConfig, animated);
        }

        public static async Task TryShowModalAsync<TViewModel>(this IModalNavigator source, TViewModel viewModel, bool? animated = null) {
            if (source == null)
                return;
            await source.ShowModalAsync(viewModel, animated);
        }

        public static async Task TryPopModalAsync(this IModalNavigator source, bool? animated = null) {
            if (source == null)
                return;
            await source.PopModalAsync(animated);
        }
    }
}
