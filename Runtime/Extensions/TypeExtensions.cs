using System;
using System.Linq;
using System.Reflection;

namespace Marx.Utilities
{
    public static class TypeExtensions{

        public static bool Implements<T>(this Type type){
            return Implements(type, typeof(T));
        }

        public static bool Implements(this Type type, Type interfaceType)
        {
            return type.GetInterface(interfaceType.Name) is not null;
        }
    
        public static bool HasAttribute<T>(this MemberInfo member) where T: Attribute
        {
            return member.GetCustomAttributes(typeof(T)).Any();
        }

        public static bool TryGetAttribute<T>(this Type type, out T attribute) where T: Attribute
        {
            var attributes = type.GetCustomAttributes(typeof(T), true);

            attribute = attributes.Length > 0 ? (T)attributes[0] : null;
            return attributes.Length > 0;
        }
    
        public static bool TryGetMethod(this Type type, string methodName, BindingFlags flags, out MethodInfo method) 
        {
            method = type.GetMethod(methodName, flags);
            return method != null;
        
        }
        
        public static bool IsStatic(this Type type) => type.IsAbstract && type.IsSealed;
    }
}