using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Blue.MVVM.Navigation;
using System.Reflection;
using System.Linq;

namespace Blue.MVVM.Navigation.ViewLocators {
    public class DeclarativeViewLocator : ImplicitViewLocator, IViewLocator {

        public async Task<Type> ResolveViewTypeForAsync(Type viewModelType, bool throwOnError = false) {
            await CrossTask.Yield();
            IncludeOriginatingAssemblyName(viewModelType);
            var typeInfo = viewModelType.GetTypeInfo();
            foreach (var assembly in Assemblies) {
                var types = assembly.GetViewTypesFor(viewModelType);
                if (types == null || types.Count() <= 0)
                    continue;

                return types.First();
            }
            return null;
        }
    }
}
