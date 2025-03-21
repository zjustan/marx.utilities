using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Marx.Utilities
{
    public class DependencyInjector
    {
        public static DependencyInjector Instance => _instance ??= new DependencyInjector();

        [StaticReload] private static DependencyInjector _instance;
        
        public readonly List<IService> Services = new();
        private readonly Dictionary<Type,Injectable> injectables = new();

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]
        static void Initialize()
        {
            
            _instance = new DependencyInjector();
        }
    
        private DependencyInjector()
        {
            _instance = this;
            
            ServiceRegister serviceRegister = new ServiceRegister();
            
            foreach (Type type in typeof(DependencyInjector).Assembly.GetReferencingAssemblies().SelectMany(x => x.GetTypes()))
            {
                if(type.IsAbstract && !type.IsStatic())
                    continue;
                
                if (type.TryGetAttribute(out ServiceAttribute serviceAttribute))
                    RegisterService(type, serviceAttribute, serviceRegister);

                RegisterInjectable(type);
            }
        }
        
        public static void Inject(object target) => Instance.InjectObject(target);

        private void InjectObject(object target)
        {
        
            var targetType = target.GetType();
            
            if(!injectables.TryGetValue(targetType, out Injectable injectable))
                return;

            foreach (InjectionField injectionPoint in injectable.InjectionPoints)
            {
                IService service = Services.FirstOrDefault(x => x.CanCreate(injectionPoint.FieldType));

                if (service != null)
                    injectionPoint.Setter(target, service.Create(target));
                else
                    Debug.LogError($"service of type {injectionPoint.FieldType.Name} could not be injected");
            }
        }

        private static void RegisterService(Type type, ServiceAttribute attribute, ServiceRegister serviceRegister)
        {
            if (!type.TryGetMethod(attribute.RegisterMethodName, BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic,
                    out MethodInfo registerMethod)) return;
            try
            {
                registerMethod.Invoke(null, new object[]{serviceRegister});
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }
        
        private void RegisterInjectable(Type type)
        {
            Injectable injectable = null;
            foreach (FieldInfo field in type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
            {
                if (!field.HasAttribute<InjectAttribute>()) continue;
                injectable ??= new Injectable();
                injectable.InjectionPoints.Add(new InjectionField(field));
            }
            
            foreach (PropertyInfo property in type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
            {
                if (!property.HasAttribute<InjectAttribute>()) continue;
                if (!property.CanWrite)
                {
                    Debug.LogError($"Property {type.Name}{property.Name} can not be written to, add a set to allow for injection");
                    continue;
                }
                
                injectable ??= new Injectable();
                injectable.InjectionPoints.Add(new InjectionField(property));
            }


            if (injectable != null)
            {
                injectables.Add(type, injectable);
            }
        }
    }
}