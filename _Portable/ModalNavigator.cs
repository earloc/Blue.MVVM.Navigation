using Blue.MVVM.IoC;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Blue.MVVM.Navigation {
    partial class ModalNavigator {

        private async Task<bool?> ShowModalCoreAsync<TViewModel>(TViewModel viewModel, Func<TViewModel, Task> asyncConfig = null) {
            throw new BaitAndSwitchException();
        }

    }
}
