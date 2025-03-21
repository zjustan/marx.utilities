using System;

namespace Marx.Utilities
{
    [System.AttributeUsage(AttributeTargets.Class)]

    public sealed class ServiceAttribute : Attribute
    {
        public readonly string RegisterMethodName;

        public ServiceAttribute(string registerName = "Register")
        {
            RegisterMethodName = registerName;
        
        }
    }
}