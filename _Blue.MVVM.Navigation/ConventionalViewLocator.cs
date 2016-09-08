using Blue.MVVM.IoC;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Blue.MVVM.Navigation {
    public partial class ConventionalViewLocator : IViewLocator {

        public ConventionalViewLocator(bool includeDefaultViewNameConvention = true) {

            if (includeDefaultViewNameConvention)
                AddViewNameConvention(new DefaultViewNameConvention());
        }

        private ISet<string> _FallbackAssemblies = new HashSet<string>();

        public void AddViewAssembly(Assembly viewAssembly) {
            if (viewAssembly == null)
                throw new ArgumentNullException(nameof(viewAssembly), "must not be null");

            _FallbackAssemblies.Add(viewAssembly.FullName);
        }

        private IList<IViewNameConvention> _Conventions = new List<IViewNameConvention>();
        public void AddViewNameConvention(IViewNameConvention convention) {
            if (convention == null)
                throw new ArgumentNullException(nameof(convention), "must not be null");
            _Conventions.Add(convention);
        }

        public Task<Type> ResolveViewTypeForAsync<TViewModel>(bool throwOnError = false) {
            return ResolveViewTypeForAsync(typeof(TViewModel), throwOnError);
        }

        public async Task<Type> ResolveViewTypeForAsync(Type viewModelType, bool throwOnError = false) {
            await CrossTask.Yield();

            IncludeOriginatingAssemblyName(viewModelType);
            
            foreach (var assemblyName in _FallbackAssemblies) {
                foreach (var convention in _Conventions) {

                    var viewName = convention.GetViewNameFor(viewModelType);
                    var viewFullName = viewName.FullName;

                    var assemblyQualifiedViewTypeName = $"{viewFullName}, {assemblyName}";

                    ResolvingView?.Invoke(this, new ResolvingViewEventArgs(viewModelType, assemblyQualifiedViewTypeName));

                    var viewType = Type.GetType(assemblyQualifiedViewTypeName);
                    if (viewType == null)
                        continue;
                    if (viewType == viewModelType)
                        continue;

                    return viewType;
                }
            }

            if (throwOnError) {
                throw new ViewNotFoundException(viewModelType);
            }
            return null;
        }

        private void IncludeOriginatingAssemblyName(Type viewModelType) {
            var viewModelFullName = viewModelType.FullName;
            var viewModelAssemblQualifiedName = viewModelType.AssemblyQualifiedName;
            var viewModelAssemblyName = viewModelAssemblQualifiedName.Replace($"{viewModelFullName}, ", string.Empty);

            if (!_FallbackAssemblies.Contains(viewModelAssemblyName))
                _FallbackAssemblies.Add(viewModelAssemblyName);
        }

        public event EventHandler<ResolvingViewEventArgs> ResolvingView;

    }
}
