using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blue.MVVM.Navigation {
    public interface IModalNavigator {
        Task ShowModalAsync<TViewModel>(Func<TViewModel, Task> asyncConfig, bool? animated = null);
        Task ShowModalAsync<TViewModel>(Action<TViewModel> config, bool? animated = null);
        Task ShowModalAsync<TViewModel>(TViewModel viewModel, bool? animated = null);
        Task ShowModalAsync<TViewModel>(bool? animated = null);

        Task PopModalAsync(bool? animated = null);
    }
}
