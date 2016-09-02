using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blue.MVVM.IoC;

namespace Blue.MVVM.Navigation {
    public abstract class CompositeViewLocator : ViewLocator {

        private ICollection<IViewLocator> _Locators = new List<IViewLocator>();

        public CompositeViewLocator(ITypeResolver typeResolver)
            : base(typeResolver) {
        }

        public void Add(IViewLocator locator) {
            if (locator == null)
                throw new ArgumentNullException(nameof(locator), "must not be null");

            _Locators.Add(locator);
        }

        public override Task<Type> ResolveViewTypeForAsync<TViewModel>() {
            foreach (var locator in _Locators) {
                var viewType = locator.ResolveViewTypeForAsync<TViewModel>();
                if (viewType != null)
                    return viewType;
            }
            return null;
        }
    }
}
