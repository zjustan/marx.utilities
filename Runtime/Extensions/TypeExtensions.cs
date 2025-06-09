using System;
using System.Linq;
using System.Reflection;

namespace Marx.Utilities
{
    public static class TypeExtensions{

        /// <summary>
        /// Determines whether the specified type implements the interface or base type of the specified generic type.
        /// </summary>
        /// <typeparam name="T">The type of the interface or base type to check against.</typeparam>
        /// <param name="type">The type to check.</param>
        /// <returns>True if the specified type implements the generic type T; otherwise, false.</returns>
        public static bool Implements<T>(this Type type) {
            return Implements(type, typeof(T));
        }

        /// <summary>
        /// Determines whether the specified type implements the given interface or base type.
        /// </summary>
        /// <param name="type">The type to check.</param>
        /// <param name="interfaceType">The interface or base type to check against.</param>
        /// <returns>True if the specified type implements the given interface or base type; otherwise, false.</returns>
        public static bool Implements(this Type type, Type interfaceType) {
            return type.GetInterface(interfaceType.Name) is not null;
        }

        /// <summary>
        /// Determines whether the specified member has a specific attribute applied.
        /// </summary>
        /// <typeparam name="T">The type of the attribute to look for.</typeparam>
        /// <param name="member">The member to inspect for the attribute.</param>
        /// <returns>True if the specified attribute is present; otherwise, false.</returns>
        public static bool HasAttribute<T>(this MemberInfo member) where T : Attribute {
            return member.GetCustomAttributes(typeof(T)).Any();
        }

        /// <summary>
        /// Attempts to retrieve a custom attribute of the specified type from the provided type.
        /// </summary>
        /// <typeparam name="T">The type of the attribute to retrieve.</typeparam>
        /// <param name="type">The type from which the attribute will be retrieved.</param>
        /// <param name="attribute">
        /// When this method returns, contains the retrieved attribute if found; otherwise, null.
        /// </param>
        /// <returns>True if the specified attribute is found on the given type; otherwise, false.</returns>
        public static bool TryGetAttribute<T>(this Type type, out T attribute) where T : Attribute {
            var attributes = type.GetCustomAttributes(typeof(T), true);

            attribute = attributes.Length > 0 ? (T)attributes[0] : null;
            return attributes.Length > 0;
        }

        /// <summary>
        /// Attempts to retrieve a method from the specified type with the given name and binding flags.
        /// </summary>
        /// <param name="type">The type from which to retrieve the method.</param>
        /// <param name="methodName">The name of the method to find.</param>
        /// <param name="flags">The binding flags used to control the search.</param>
        /// <param name="method">When this method returns, contains the MethodInfo object representing the method if found; otherwise, null.</param>
        /// <returns>True if the method is found; otherwise, false.</returns>
        public static bool TryGetMethod(this Type type, string methodName, BindingFlags flags, out MethodInfo method) {
            method = type.GetMethod(methodName, flags);
            return method != null;
        }

        /// <summary>
        /// Determines whether the specified type is static.
        /// </summary>
        /// <param name="type">The type to check if it is static.</param>
        /// <returns>True if the specified type is static; otherwise, false.</returns>
        public static bool IsStatic(this Type type) => type.IsAbstract && type.IsSealed;
    }
}