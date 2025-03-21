using System;
using Object = UnityEngine.Object;

namespace Marx.Utilities
{
    public interface IServiceCreator
    {
        IServiceCreator AsSingleton();
        IServiceCreator AsSceneScoped();
        IServiceCreator AsScoped();
        IServiceCreator From(Func<object> factory);
        IServiceCreator From(Func<object,object> factory);
    }
}