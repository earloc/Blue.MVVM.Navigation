using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Blue.MVVM.Navigation {
    public class CrossType {

        private Type _Type;

#if !NET40
        private TypeInfo _Info;
#endif

        public CrossType(Type type) {
            if (type == null)
                throw new ArgumentNullException(nameof(type), "must not be null");
            _Type = type;

#if !NET40
            _Info = type.GetTypeInfo();
#endif

        }

        public bool IsGenericTypeDefinition {
            get {
#if NET40
                return _Type.IsGenericTypeDefinition;
#else
                return _Info.IsGenericTypeDefinition;
#endif
            }
        }

        public bool IsGenericType {
            get {
#if NET40
                return _Type.IsGenericType;
#else
                return _Info.IsGenericType;
#endif
            }
        }
        
        public CrossType MakeGenericType(Type[] typeArguments) {

            return new CrossType(
#if NET40
                _Type.MakeGenericType(typeArguments)
#else
                _Info.MakeGenericType(typeArguments)
#endif
            );
        }

        public Type[] GetGenericArguments() {

#if WINDOWS_UWP
            return _Type.GetGenericArguments();
#elif INCLUD_REFLECTION_STUB
            return _Type.GenericTypeArguments;
#else
            return _Type.GetGenericArguments();
#endif
            throw new PlatformNotSupportedException();
        }

        internal bool IsAssignableFrom(CrossType b) {
#if NET40
                return _Type.IsAssignableFrom(b._Type);
#else
            return _Info.IsAssignableFrom(b._Info);
#endif


        }
    }

    public static partial class TypeExtensions {
        public static CrossType AsCrossType(this Type source) {
            return new CrossType(source);
        }
    }
}
