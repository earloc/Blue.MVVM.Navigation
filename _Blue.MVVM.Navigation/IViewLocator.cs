using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace StarFlash.Client.Contracts {
    public interface IViewLocator {
        Task<TView> ResolveViewForAsync<TView, TViewModel>(Action<TViewModel> viewModelConfiguration = null);
        Task<TView> ResolveViewForAsync<TView, TViewModel>(TViewModel viewModel);
    }
}
