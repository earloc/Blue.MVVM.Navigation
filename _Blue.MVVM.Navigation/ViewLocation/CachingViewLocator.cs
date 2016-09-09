using Blue.MVVM.Navigation;
using Blue.MVVM.Navigation.ViewLocators;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blue.MVVM.Navigation.ViewLocators {
    public class CachingViewLocator : IViewLocator {

        private readonly IViewLocator _BaseLocator;

        public CachingViewLocator(IViewLocator baseLocator) {
            if (baseLocator == null)
                throw new ArgumentNullException(nameof(baseLocator), "must not be null");

            _BaseLocator = baseLocator;
        }


        private Dictionary<Type, Type> _Cache = new Dictionary<Type, Type>();

        public async Task<Type> ResolveViewTypeForAsync(Type viewModelType, bool throwOnError = false) {
            if (_Cache.ContainsKey(viewModelType))
                return _Cache[viewModelType];

            var viewType = await _BaseLocator.ResolveViewTypeForAsync(viewModelType, false);

            if (viewType != null) {
                _Cache[viewModelType] = viewType;
                return viewType;
            }

            if (throwOnError)
                throw new ViewNotFoundException(viewModelType);

            return null;
        }
    }
}
