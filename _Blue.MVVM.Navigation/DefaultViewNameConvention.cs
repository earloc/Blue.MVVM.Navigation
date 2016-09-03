using System;
using System.Collections.Generic;
using System.Text;

namespace Blue.MVVM.Navigation {
    class DefaultViewNameConvention : IViewNameConvention {
        public ViewName GetViewNameFor<TViewModel>() {
            var viewModelType = typeof(TViewModel);

            var viewModelNameSpace = viewModelType.Namespace;
            var viewNameSpace = viewModelNameSpace.Replace("ViewModels", "Views");

            var viewModelSimpleName = viewModelType.Name;
            var viewSimpleName = viewModelSimpleName.Replace("ViewModel", "");

            return new ViewName(viewNameSpace, viewSimpleName);
        }
    }
}
