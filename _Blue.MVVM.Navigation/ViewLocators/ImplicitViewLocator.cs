using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Linq;
namespace Blue.MVVM.Navigation.ViewLocators {
    public abstract class ImplicitViewLocator {

        private ISet<Assembly> _ObservedAssemblies = new HashSet<Assembly>();

        public void AddViewAssembly(Assembly viewAssembly) {
            if (viewAssembly == null)
                throw new ArgumentNullException(nameof(viewAssembly), "must not be null");

            _ObservedAssemblies.Add(viewAssembly);
            AssemblyNames = _ObservedAssemblies.Select(x => x.FullName).ToArray();
        }

        protected IEnumerable<string> AssemblyNames {
            get; private set;
        }

        protected IEnumerable<Assembly> Assemblies {
            get {
                return _ObservedAssemblies;
            }
        }
        protected void IncludeOriginatingAssemblyName(Type viewModelType) {
            var assembly = viewModelType.GetTypeInfo().Assembly;

            if (!_ObservedAssemblies.Contains(assembly)) {
                AddViewAssembly(assembly);
            }

        }
    }
}
