using System;
using System.Collections.Generic;
using System.Text;

namespace Blue.MVVM.Navigation {
    public interface IViewNameConvention {

        ViewName GetViewNameFor<TViewModel>();
        ViewName GetViewNameFor(Type viewModelType);

    }
}
