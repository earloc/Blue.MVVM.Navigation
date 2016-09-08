using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blue.MVVM.Navigation {
    public interface INavigationAwareView {

        Task NavigatingToAsync();

        Task NavigatedToAsync();

        Task NavigatingFromAsync();

        Task NavigatedFromAsync();
    }

    public static class INavigationAwareViewExtensions {
        public static async Task TryNavigatingToAsync(this INavigationAwareView source) {
            if (source == null)
                return;
            await source.NavigatingFromAsync();
        }

        public static async Task TryNavigatedToAsync(this INavigationAwareView source) {
            if (source == null)
                return;
            await source.NavigatedToAsync();
        }

        public static async Task TryNavigatingFromAsync(this INavigationAwareView source) {
            if (source == null)
                return;
            await source.NavigatingFromAsync();
        }

        public static async Task TryNavigatedFromAsync(this INavigationAwareView source) {
            if (source == null)
                return;
            await source.NavigatedFromAsync();
        }
    }
}
