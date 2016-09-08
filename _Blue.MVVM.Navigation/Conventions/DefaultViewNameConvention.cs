using System;
using System.Collections.Generic;
using System.Text;

namespace Blue.MVVM.Navigation.Conventions {
    public partial class DefaultViewNameConvention : IViewNameConvention {
        public IEnumerable<ViewName> GetPossibleViewNamesFor(Type viewModelType) {
            if (viewModelType == null)
                throw new ArgumentNullException(nameof(viewModelType), "must not be null");

            var viewModelNameSpace = viewModelType.Namespace;
            var viewNameSpace = viewModelNameSpace.Replace("ViewModel", "View");

            var viewModelSimpleName = viewModelType.Name;

            var viewSimpleName = viewModelSimpleName.Replace("ViewModel", "");
            return new ViewName[] { new ViewName(viewNameSpace, viewSimpleName) };
        }
    }
}
