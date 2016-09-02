using Blue.MVVM.IoC;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Blue.MVVM.Navigation {
    public class ConventionalViewLocator : IViewLocator {

        public ConventionalViewLocator(bool includeDefaultViewNameConvention = true) {

            if (includeDefaultViewNameConvention)
                AddViewNameConvention(new DefaultViewNameConvention());
        }

        private IDictionary<string, Assembly> _ViewAssemblies = new Dictionary<string, Assembly>();
        public void AddViewAssembly(Assembly viewAssembly) {
            if (viewAssembly == null)
                throw new ArgumentNullException(nameof(viewAssembly), "must not be null");

            _ViewAssemblies.Add(viewAssembly.FullName, viewAssembly);
        }

        private IList<IViewNameConvention> _Conventions = new List<IViewNameConvention>();
        public void AddViewNameConvention(IViewNameConvention convention) {
            if (convention == null)
                throw new ArgumentNullException(nameof(convention), "must not be null");
            _Conventions.Add(convention);
        }

        public async Task<Type> ResolveViewTypeForAsync<TViewModel>() {

            await Task.Yield();
            foreach (var assembly in _ViewAssemblies) {

                foreach (var convention in _Conventions) {
                    var viewName = convention.GetViewNameFor<TViewModel>();
                    var viewFullName = viewName.FullName;


                    var assemblyName = assembly.Value.FullName;
                    var assemblyQualifiedViewTypeName = $"{viewFullName}, {assemblyName}";
                    var viewType = Type.GetType(assemblyQualifiedViewTypeName);
                    if (viewType != null)
                        return viewType;
                }
            }

            return null;
        }
    }
}
