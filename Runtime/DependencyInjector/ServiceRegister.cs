using System;

namespace Marx.Utilities
{
    public class ServiceRegister
    {
        public IServiceCreator Bind<TResult>() where TResult : class
        {
            return (Service<TResult>)Bind(typeof(TResult));
        }
        
        public IServiceCreator Bind(Type resultType, params Type[] aliases)
        {
            Type serviceType = typeof(Service<>).MakeGenericType(resultType);
            var service = (IService)Activator.CreateInstance(serviceType,new object[] {aliases});
            DependencyInjector.Instance.Services.Add(service);
            return (IServiceCreator)service;
        }
        
    }
}