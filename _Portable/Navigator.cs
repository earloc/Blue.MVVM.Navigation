using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Blue.MVVM.Navigation {
    public partial class Navigator {

        private void SetPlatformViewModel<TView, TViewModel, TIgnored>(TView view, TViewModel viewModel, Action<TIgnored, TViewModel> ignored) {
            throw new BaitAndSwitchException();
        }

        public Task<bool> PushCoreAsync<TViewModel>(TViewModel viewModel, Func<TViewModel, Task> asyncConfig = null) {
            throw new BaitAndSwitchException();
        }

        public Task PopAsync() {
            throw new BaitAndSwitchException();
        }
    }
}
