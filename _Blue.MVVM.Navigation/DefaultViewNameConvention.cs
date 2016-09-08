using System;
using System.Collections.Generic;
using System.Text;

namespace Blue.MVVM.Navigation {
    public class DefaultViewNameConvention : IViewNameConvention {
        public ViewName GetViewNameFor(Type viewModelType) {
            if (viewModelType == null)
                throw new ArgumentNullException(nameof(viewModelType), "must not be null");

            var viewModelNameSpace = viewModelType.Namespace;
            var viewNameSpace = viewModelNameSpace.Replace("ViewModel", "View");

            var viewModelSimpleName = viewModelType.Name;

            var viewSimpleName = viewModelSimpleName.Replace("ViewModel", "");

            return new ViewName(viewNameSpace, viewSimpleName);
        }

        public ViewName GetViewNameFor<TViewModel>() {
            return GetViewNameFor(typeof(TViewModel));
        }
    }
}
