using System;
using UnityEngine;

namespace Marx.Utilities
{

    public abstract class Factory : ScriptableObject, IFactory
    {
        public int Index { get; set; }

        public int TotalCount { get; set; }
        public abstract Type FactoryType { get; }
    }

    public abstract class Factory<T> : Factory
    {
        public abstract T Construct();
        public override sealed Type FactoryType => typeof(T);
    }

    public interface IFactory { }

}