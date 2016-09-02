using Blue.MVVM.IoC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blue.MVVM.Navigation {
    public class MappingViewLocator : ViewLocator {

        private Dictionary<Type, Type> _Map = new Dictionary<Type, Type>();

        public MappingViewLocator(ITypeResolver container)
            : base(container) {
        }

        public void Add<TViewModel, TView>() {
            _Map.Add(typeof(TViewModel), typeof(TView));
        }

        public override async Task<Type> ResolveViewTypeForAsync<TViewModel>() {
            await Task.Yield();

            var key = typeof(TViewModel);
            if (_Map.ContainsKey(key))
                return _Map[key];

            return null;
        }
    }
}
