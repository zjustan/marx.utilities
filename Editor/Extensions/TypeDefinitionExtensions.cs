using System;
using Mono.Cecil;

namespace Marx.Utilities.Editor
{
    public static class TypeDefinitionExtensions{
        
        public static bool InheritsFrom<T>(this TypeDefinition typeDefinition) => InheritsFrom(typeDefinition, typeof(T));
        public static bool InheritsFrom(this TypeDefinition typeDefinition, Type other)
        {
            int inheritLimit = 100;

            while (inheritLimit-- > 0)
            {
                if(typeDefinition.FullName == other.FullName)
                    return true;

                if (typeDefinition.BaseType == null)
                    return false;
            
            
                typeDefinition = typeDefinition.BaseType.Resolve();
            }

            return false;
        }
    }
}