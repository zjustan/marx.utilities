using System;

namespace Marx.Utilities
{
    /// <summary>
    /// Allows for the dependency injector to inject this field with a service that has been registered
    /// </summary>
    [System.AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public sealed class InjectAttribute : Attribute { }
}