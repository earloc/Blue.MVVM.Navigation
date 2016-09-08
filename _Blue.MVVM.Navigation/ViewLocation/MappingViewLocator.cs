using Blue.MVVM.IoC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blue.MVVM.Navigation.ViewLocators {
    public class MappingViewLocator : IViewLocator {

        private Dictionary<Type, Type> _Map = new Dictionary<Type, Type>();

        public void Add<TViewModel, TView>() {
            _Map.Add(typeof(TViewModel), typeof(TView));
        }

        public async Task<Type> ResolveViewTypeForAsync(Type viewModelType, bool throwOnError = false) {
            await CrossTask.Yield();

            var key = viewModelType;
            if (_Map.ContainsKey(key))
                return _Map[key];

            if (throwOnError) {
                throw new ViewNotFoundException(key);
            }
            return null;
        }

        public Task<Type> ResolveViewTypeForAsync<TViewModel>(bool throwOnError = false) {
            return ResolveViewTypeForAsync(typeof(TViewModel), throwOnError);
        }
    }
}
