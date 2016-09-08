using System;
using System.Collections.Generic;
using System.Text;

namespace Blue.MVVM.Navigation.Conventions {
    public interface IViewNameConvention {

        IEnumerable<ViewName> GetPossibleViewNamesFor(Type viewModelType);
    }

    public static class IViewNameConventionExtensions {
       public static IEnumerable<ViewName> GetPossibleViewNamesFor<TViewModel>(this IViewNameConvention source) {
            return source.GetPossibleViewNamesFor(typeof(TViewModel));
        }
    }
}
