using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blue.MVVM.Navigation {
    public interface INavigator {
        Task<bool> PushAsync<TViewModel>(Func<TViewModel, Task> asyncConfig) where TViewModel : class;
        Task<bool> PushAsync<TViewModel>(Action<TViewModel> config) where TViewModel : class;
        Task<bool> PushAsync<TViewModel>(TViewModel viewModel) where TViewModel : class;
        Task<bool> PushAsync<TViewModel>() where TViewModel : class;
        Task PopAsync();
    }
}
