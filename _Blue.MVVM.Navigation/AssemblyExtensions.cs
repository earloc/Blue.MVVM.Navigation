using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Linq;
using Blue.MVVM.Navigation.ViewLocators;

namespace Blue.MVVM.Navigation {
    internal static class AssemblyExtensions {
        private class Match {
            public Match(CrossType a, CrossType b, Type viewType) {
                Rank = 0;
                if (a.IsGenericTypeDefinition) {
                    Rank = 1;
                    if (b.IsGenericType) {
                        a = a.MakeGenericType(b.GetGenericArguments());
                    }
                }
                if (a.IsAssignableFrom(b)) {
                    Type = viewType;
                }

            }
            public bool IsMatch => Type != null;

            public Type Type { get; }

            public int Rank { get; }

        }

        public static IEnumerable<Type> GetViewTypesFor(this Assembly source, Type viewModelType) {
            var types = source.GetTypes();
            var viewModelCrossType = viewModelType.AsCrossType();
            var viewTypes = (from type in types
                            let attributes = type.GetTypeInfo().GetCustomAttributes(typeof(DefaultViewForAttribute), false).OfType<DefaultViewForAttribute>()
                            from atr in attributes
                            let match = new Match(atr.ViewModelType.AsCrossType(), viewModelCrossType, type)
                            where match.IsMatch
                            select match);
            return viewTypes.OrderBy(x => x.Rank).Select(x => x.Type);

        }

#if INCLUD_REFLECTION_STUB
        public static Type[] GetTypes(this Assembly source) {
            return source.DefinedTypes.Select(x => x.AsType()).ToArray();
        }
#endif
    }
}
