using System;

namespace Marx.Utilities
{
    public interface IService
    {
        public object Create(object target);
        bool CanCreate(Type type);
    }
}