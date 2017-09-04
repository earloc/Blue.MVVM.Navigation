using Blue.MVVM.IoC;
using Blue.MVVM.Navigation.ViewLocators;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blue.MVVM.Navigation {
    partial class ModalNavigator : NavigatorBase, IModalNavigator {

        public class Settings {
            public bool IsAnimationEnabled { get; set; }
        }

        public static Settings DefaultSettings { get; } = new Settings();

        protected ModalNavigator(IViewLocator viewLocator, IServiceLocator serviceLocator)
            : base(viewLocator, serviceLocator) {
        }

        public async Task ShowModalAsync<TViewModel>(bool? animated = null) where TViewModel : class {
            var viewModel = ServiceLocator.Get<TViewModel>();
            await ShowModalCoreAsync(viewModel, animated: animated);
        }

        public async Task ShowModalAsync<TViewModel>(Action<TViewModel> config = null, bool? animated = null) where TViewModel : class {
            var viewModel = ServiceLocator.Get<TViewModel>();
            await ShowModalCoreAsync(viewModel, async vm => {
                await CrossTask.Yield();
                config?.Invoke(vm);
            }, animated);
        }

        public async Task ShowModalAsync<TViewModel>(Func<TViewModel, Task> asyncConfig = null, bool? animated = null) where TViewModel : class {
            var viewModel = ServiceLocator.Get<TViewModel>();
            await ShowModalCoreAsync(viewModel, asyncConfig, animated);
        }

        public async Task ShowModalAsync<TViewModel>(TViewModel viewModel, bool? animated = null) where TViewModel : class {
            await ShowModalCoreAsync(viewModel, animated: animated);
        }
    }
}
