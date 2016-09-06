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
}
