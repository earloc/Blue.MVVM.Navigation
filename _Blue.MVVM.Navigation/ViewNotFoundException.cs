using System;
using System.Collections.Generic;
using System.Text;

namespace Blue.MVVM.Navigation {
    public class ViewNotFoundException : Exception {
        public ViewNotFoundException(Type viewModelType) 
            : base ($"No view found to present '{viewModelType?.FullName??"Unknown"}' with") {
        }
    }
}
