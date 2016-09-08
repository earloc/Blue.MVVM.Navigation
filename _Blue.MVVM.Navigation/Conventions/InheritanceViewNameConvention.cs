using Blue.MVVM.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace Blue.MVVM.Navigation.Conventions {
    public class InheritanceViewNameConvention : IViewNameConvention {

        private IViewNameConvention _BaseConvention;
        public InheritanceViewNameConvention(IViewNameConvention baseConvention) {
            if (baseConvention == null)
                throw new ArgumentNullException(nameof(baseConvention), "must not be null");

            _BaseConvention = baseConvention;
        }

        public IEnumerable<ViewName> GetPossibleViewNamesFor(Type viewModelType) {
            var names = new List<ViewName>();

            var type = viewModelType;
            
            while (type != null) {
                names.AddRange(_BaseConvention.GetPossibleViewNamesFor(type));
                type = type.GetTypeInfo().BaseType;
            }

            return names;
        }
    }
}
