using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blue.MVVM.IoC;

namespace Blue.MVVM.Navigation {
    public class CompositeViewLocator : IViewLocator {

        private ICollection<IViewLocator> _Locators = new List<IViewLocator>();


        public void Add(IViewLocator locator) {
            if (locator == null)
                throw new ArgumentNullException(nameof(locator), "must not be null");

            _Locators.Add(locator);
        }

        public Task<Type> ResolveViewTypeForAsync<TViewModel>(bool throwOnError = false) {
            return ResolveViewTypeForAsync(typeof(TViewModel), throwOnError);
        }

        public async Task<Type> ResolveViewTypeForAsync(Type viewModelType, bool throwOnError = false) {
            foreach (var locator in _Locators) {
                var viewType = await locator.ResolveViewTypeForAsync(viewModelType);
                if (viewType != null)
                    return viewType;
            }
            if (throwOnError) {
                throw new ViewNotFoundException(viewModelType);
            }
            return null;
        }

       
    }
}
