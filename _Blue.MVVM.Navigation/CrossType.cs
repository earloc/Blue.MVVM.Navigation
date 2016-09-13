using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Blue.MVVM.Navigation {
    public class CrossType {

        private Type _Type;

        private TypeInfo _Info;

        public CrossType(Type type) {
            if (type == null)
                throw new ArgumentNullException(nameof(type), "must not be null");
            _Type = type;

            _Info = type.GetTypeInfo();

        }

        public bool IsGenericTypeDefinition {
            get {
                return _Info.IsGenericTypeDefinition;
            }
        }

        public bool IsGenericType {
            get {
                return _Info.IsGenericType;
            }
        }
        
        public CrossType MakeGenericType(Type[] typeArguments) {
            return new CrossType(_Info.MakeGenericType(typeArguments));
        }

        public Type[] GetGenericArguments() {

#if WINDOWS_UWP
            return _Type.GetGenericArguments();
# elif INCLUD_REFLECTION_STUB
            return _Type.GenericTypeArguments;
#else
            return _Type.GetGenericArguments();
#endif
            throw new PlatformNotSupportedException();
        }

        internal bool IsAssignableFrom(CrossType b) {
            return _Info.IsAssignableFrom(b._Info);
        }
    }

    public static class TypeExtensions {
        public static CrossType AsCrossType(this Type source) {
            return new CrossType(source);
        }
    }
}
