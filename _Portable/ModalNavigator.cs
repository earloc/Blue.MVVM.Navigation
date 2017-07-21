using Blue.MVVM.IoC;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blue.MVVM.Navigation {
    public partial class ModalNavigator {

        private Task ShowModalCoreAsync<TViewModel>(TViewModel viewModel, Func<TViewModel, Task> asyncConfig = null, bool? animated = null) {
            throw new BaitAndSwitchException();
        }

        public Task PopModalAsync(bool? animated = null) {
            throw new BaitAndSwitchException();
        }
    }
}
