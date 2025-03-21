using System;
using UnityEngine;

namespace Marx.Utilities
{

    public abstract class Factory : ScriptableObject
    {
        public int Index { get; set; }

        public int TotalCount { get; set; }
        public abstract Type FactoryType { get; }
    }

    public abstract class Factory<T> : Factory, IFactory<T>
    {
        public abstract T Construct();
        public sealed override Type FactoryType => typeof(T);
    }

    public interface IFactory { }

    public interface IFactory<out TOut> : IFactory
    {
        TOut Construct();
    }
    
    public interface IFactory<in T0, out TOut> : IFactory
    {
        TOut Construct(T0 arg0);
    }
    
    public interface IFactory<in T0, in T1, out TOut> : IFactory
    {
        TOut Construct(T0 arg0, T1 arg1);
    }
    
    public interface IFactory<in T0, in T1, in T2, out TOut> : IFactory
    {
        TOut Construct(T0 arg0, T1 arg1, T2 arg2);
    }
    
    public interface IFactory<in T0, in T1, in T2, in T3, out TOut> : IFactory
    {
        TOut Construct(T0 arg0, T1 arg1, T2 arg2, T3 arg3);
    }
}