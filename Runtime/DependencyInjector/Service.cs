using System;
using System.Collections.Generic;
using System.Linq;
using Object = UnityEngine.Object;

namespace Marx.Utilities
{
    public class Service<T> : IService, IServiceCreator
    {
        private Type[] aliases;

        private Type contractType;
    
        private Func<object,T> factory;

        private List<(IContract contract, T result)> Objects = new();
        public Service(params Type[] types)
        {
            aliases = types;
        }

        public IServiceCreator AsSingleton()
        {
            contractType = typeof(SingletonContract);
            return this;
        }

        public IServiceCreator AsSceneScoped()
        {
            contractType =  typeof(SceneScopedContract);
            return this;
        }
    
        public IServiceCreator AsScoped()
        {
            contractType =  typeof(ScopedContract);
            return this;
        }

        public IServiceCreator From(Func<object> factory)
        {
            this.factory = (x) => (T)factory();
            return this;
        }
    
        public IServiceCreator From(Func<object,object> factory)
        {
            this.factory = (x) => (T)factory(x);
            return this;
        }

        public object Create(object target) => create(target);
        public bool CanCreate(Type type)
        {
            if (typeof(T) == type)
                return true;
        
            if(aliases.Contains(type))
                return true;

            return false;
        }

        private T create(object target)
        {
            for (int i = 0; i < Objects.Count; i++)
            {
                if (Objects[i].contract.IsValidOn(target))
                {
                    return Objects[i].result;
                }
            }
            T value = factory(target);
            IContract contract = (IContract)Activator.CreateInstance(contractType);
            contract.Setup(target);
            Objects.Add((contract, value));
            return value;
        }


    }
}