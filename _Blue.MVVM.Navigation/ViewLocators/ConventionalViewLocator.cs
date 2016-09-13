using Blue.MVVM.IoC;
using Blue.MVVM.Navigation.Conventions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Blue.MVVM.Navigation.ViewLocators {
    public partial class ConventionalViewLocator : ImplicitViewLocator, IViewLocator {

        public ConventionalViewLocator(bool includeDefaultViewNameConvention = true) {

            if (includeDefaultViewNameConvention)
                AddViewNameConvention(new InheritanceViewNameConvention(new DefaultViewNameConvention()));
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
            
            foreach (var assemblyName in AssemblyNames) {
                foreach (var convention in _Conventions) {
                    foreach (var viewName in convention.GetPossibleViewNamesFor(viewModelType)) {
                        var viewFullName = viewName.FullName;

                        var assemblyQualifiedViewTypeName = $"{viewFullName}, {assemblyName}";

                        OnResolvingView(viewModelType, assemblyQualifiedViewTypeName);

                        var viewType = Type.GetType(assemblyQualifiedViewTypeName);
                        if (viewType == null)
                            continue;
                        if (viewType == viewModelType)
                            continue;

                        return viewType;
                    }
                }
            }

            if (throwOnError) {
                throw new ViewNotFoundException(viewModelType);
            }
            return null;
        }

        protected void OnResolvingView(Type viewModelType, string assemblyQualifiedViewTypeName) {
            ResolvingView?.Invoke(this, new ResolvingViewEventArgs(viewModelType, assemblyQualifiedViewTypeName));
        }

        public event EventHandler<ResolvingViewEventArgs> ResolvingView;

    }
}
