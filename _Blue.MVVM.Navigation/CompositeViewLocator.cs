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

        public async Task<Type> ResolveViewTypeForAsync<TViewModel>() {
            foreach (var locator in _Locators) {
                var viewType = await locator.ResolveViewTypeForAsync<TViewModel>();
                if (viewType != null)
                    return viewType;
            }
            return null;
        }
    }
}
