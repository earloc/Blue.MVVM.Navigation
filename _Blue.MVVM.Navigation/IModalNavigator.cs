using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blue.MVVM.Navigation {
    public interface IModalNavigator {
        Task<bool?> ShowModalAsync<TViewModel>(Func<TViewModel, Task> asyncConfig);
        Task<bool?> ShowModalAsync<TViewModel>(Action<TViewModel> config);
        Task<bool?> ShowModalAsync<TViewModel>(TViewModel viewModel);
        Task<bool?> ShowModalAsync<TViewModel>();
    }
}
