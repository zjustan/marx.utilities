using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Marx.Utilities
{
    public static class AssemblyExtensions
    {
        /// <summary>
        /// Retrieves all assemblies that reference the specified assembly,
        /// including the assembly itself.
        /// </summary>
        /// <param name="assembly">
        /// The assembly for which referencing assemblies are to be retrieved.
        /// </param>
        /// <returns>
        /// A collection of assemblies that reference the specified assembly,
        /// including the assembly itself.
        /// </returns>
        public static IEnumerable<Assembly> GetReferencingAssemblies(this Assembly assembly) {
            AssemblyName currentAssemblyName = assembly.GetName();
            Assembly[] allAssemblies = AppDomain.CurrentDomain.GetAssemblies();
            return allAssemblies
                .Where(x => x.GetReferencedAssemblies()
                    .Any(y => AssemblyName.ReferenceMatchesDefinition(y, currentAssemblyName)))
                .Append(assembly);
        }
    }
}