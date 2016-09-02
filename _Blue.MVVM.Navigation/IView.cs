using System;
using System.Collections.Generic;
using System.Text;

namespace Blue.MVVM.Navigation {
    public interface IView<TViewModel> {
        TViewModel ViewModel { get; set; }
    }
}
