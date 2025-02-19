using System;

namespace Marx.Utilities 
{
    /// <summary>
    /// If domain reload is disabled, static fields and properties will not reset between playmodes.
    /// By adding this attribute to a field/property it will reset regardless of whether domain reload is enabled or disabled.
    /// If possible you want to have domain reload disabled since it causes very long load times when entering play mode.
    /// For instructions on how to disable domain reload check the summary in the StaticReloader class.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class StaticReloadAttribute : Attribute { }
}