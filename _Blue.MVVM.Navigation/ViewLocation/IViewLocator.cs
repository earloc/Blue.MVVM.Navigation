using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blue.MVVM.Navigation.ViewLocators {
    public interface IViewLocator {

        Task<Type> ResolveViewTypeForAsync(Type viewModelType, bool throwOnError = false);
    }
}
