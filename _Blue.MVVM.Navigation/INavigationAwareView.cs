using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blue.MVVM.Navigation {
    public interface INavigationAwareView {

        Task AppearingAsync();

        Task AppearedAsync();

        Task DisappearingAsync();

        Task DisappearedAsync();
    }

    public static class INavigationAwareViewExtensions {
        public static async Task TryAppearingAsync(this INavigationAwareView source) {
            if (source == null)
                return;
            await source.AppearingAsync();
        }

        public static async Task TryAppearedAsync(this INavigationAwareView source) {
            if (source == null)
                return;
            await source.AppearedAsync();
        }

        public static async Task TryDisappearingAsync(this INavigationAwareView source) {
            if (source == null)
                return;
            await source.DisappearingAsync();
        }

        public static async Task TryDisappearedAsync(this INavigationAwareView source) {
            if (source == null)
                return;
            await source.DisappearedAsync();
        }
    }
}
