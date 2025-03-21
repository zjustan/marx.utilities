using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Marx.Utilities
{
    public static class AssemblyExtensions
    {
        public static IEnumerable<Assembly> GetReferencingAssemblies(this Assembly assembly)
        {
            AssemblyName currentAssemblyName = assembly.GetName();
            Assembly[] allAssemblies = AppDomain.CurrentDomain.GetAssemblies();
            return allAssemblies
                .Where(x => x.GetReferencedAssemblies()
                    .Any(y => AssemblyName.ReferenceMatchesDefinition(y, currentAssemblyName)))
                .Append(assembly);
        }
    }
}