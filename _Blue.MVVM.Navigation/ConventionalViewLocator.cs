using Blue.MVVM.IoC;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Blue.MVVM.Navigation {
    public abstract class ConventionalViewLocator : ViewLocator {

        public ConventionalViewLocator(ITypeResolver typeResolver)
            : base(typeResolver) {
        }
        private IDictionary<string, Assembly> _ViewAssemblies = new Dictionary<string, Assembly>();
        public void AddViewAssembly(Assembly viewAssembly) {
            if (viewAssembly == null)
                throw new ArgumentNullException(nameof(viewAssembly), "must not be null");

            _ViewAssemblies.Add(viewAssembly.FullName, viewAssembly);
        }

        public override async Task<Type> ResolveViewTypeForAsync<TViewModel>() {

            await Task.Yield();

            var viewModelType = typeof(TViewModel);

            var viewModelNameSpace = viewModelType.Name;
            var viewNameSpace = viewModelNameSpace.Replace("ViewModels", "Views");

            var viewModelSimpleName = viewModelType.Name;
            var viewSimpleName = viewModelSimpleName.Replace("ViewModel", "");

            var viewFullName = $"{viewNameSpace}.{viewSimpleName}";

            foreach (var assembly in _ViewAssemblies) {
                var assemblyName = assembly.Value.FullName;
                var assemblyQualifiedViewTypeName = $"{viewFullName}, {assemblyName}";
                var viewType = Type.GetType(assemblyQualifiedViewTypeName);
                if (viewType != null)
                    return viewType;
            }

            return null;
        }
    }
}
