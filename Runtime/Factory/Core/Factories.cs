using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Marx.Utilities
{

    public static class Factories
    {
        static Dictionary<Type, Factory> factories
        {
            get
            {
                factorieCache ??= Resources.LoadAll<Factory>("").ToDictionary(x => x.FactoryType, x => x);
                return factorieCache;
            }
        }

        static Dictionary<Type, Factory> factorieCache;

        private static Dictionary<object, FactoryCollection> factoryCollections = new();
        public static FactoryCollection Collection(object obj)
        {
            if (factoryCollections.TryGetValue(obj, out FactoryCollection result))
                return result;

            result = new();
            factoryCollections.Add(obj, result);
            return result;
        }

        public static T Construct<T>(params object[] objects)
        {
            Factory<T> factory = GetFactory<T>();
            return Construct(factory, objects);
        }

        public static T Construct<T>(Factory<T> factory, params object[] objects)
        {
            if (objects is null || objects.Length == 0)
                return factory.Construct();

            MethodInfo method = factory.GetType().GetMethods()
                .Where(x => x.Name == "Construct")
                .Where(x => CanResolve(x, objects)).FirstOrDefault() ?? throw new Exception("Construct method failed to invoke, parameters where passed incorrectly. Make sure the call has the parameters in the same order as in the function");

            return (T)method.Invoke(factory, objects);
        }

        public static List<T> ConstructMultiple<T>(int amount, params object[] objects)
        {
            Factory<T> factory = GetFactory<T>();

            List<T> results = new(amount);
            factory.TotalCount = amount;
            for (int i = 0; i < amount; i++)
            {
                results.Add(Construct<T>(factory, objects));
                factory.Index++;
            }
            return results;
        }

        public static Factory<T> GetFactory<T>()
        {
            if (factories.TryGetValue(typeof(T), out Factory factory) && factory is Factory<T> result)
            {
                result.Index = 0;
                return result;
            }

            throw new MissingReferenceException($"ScriptableObject for factory: {typeof(Factory<T>).Name} of type: {typeof(T).Name} does not exist. Make sure you have created a factory for {typeof(T).Name}.");
        }

        private static bool CanResolve(MethodInfo info, object[] objects)
        {
            ParameterInfo[] parameters = info.GetParameters();

            if (parameters.Length == 0 || parameters.Length != objects.Length)
                return false;

            for (int i = 0; i < parameters.Length; i++)
            {
                if (objects[i] is null)
                {
                    if (IsNullableType(parameters[i].ParameterType))
                        continue;
                    else
                        return false;
                }

                Type objType = objects[i].GetType();
                if (!Equals(parameters[i].ParameterType, objType))
                    return false;
            }

            return true;
        }

        private static bool IsNullableType(this Type type)
        {
            if (type.GetTypeInfo().IsGenericType)
            {
                return type.GetGenericTypeDefinition() == typeof(Nullable<>);
            }

            return false;
        }

    }

}