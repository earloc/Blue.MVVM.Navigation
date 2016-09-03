using System;
using System.Collections.Generic;
using System.Text;

namespace Blue.MVVM.Navigation {
    public class ViewNotFoundException<TViewModel> : Exception {
        public ViewNotFoundException() 
            : base ($"No view found to present '{typeof(TViewModel).FullName}' with") {
        }
    }
}
