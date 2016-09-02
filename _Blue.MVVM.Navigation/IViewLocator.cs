using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blue.MVVM.Navigation {
    public interface IViewLocator {
        Task<TView> ResolveViewForAsync<TViewModel, TView>(Action<TViewModel> viewModelConfiguration = null);
        Task<TView> ResolveViewForAsync<TViewModel, TView>(TViewModel viewModel);

        Task<Type> ResolveViewTypeForAsync<TViewModel>();
    }
}
