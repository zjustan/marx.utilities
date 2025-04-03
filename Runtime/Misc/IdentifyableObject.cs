using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Marx.Utilities
{

    public class IdentifyableObject<T> : ScriptableObject, IEquatable<T> where T : IdentifyableObject<T>
    {
        [field: SerializeField] public SGuid Guid { get; private set; }

        public static T Get(SGuid id)
        {
            Lookup.TryGetValue(id, out var result);
            return result;
        }

        public static IReadOnlyCollection<T> All
        {
            get
            {
                FillCache();
                return lookupCache.Values;
            }
        }

        public static IReadOnlyDictionary<SGuid, T> Lookup
        {
            get
            {
                FillCache();
                return lookupCache;
            }

        }

        private static Dictionary<SGuid, T> lookupCache;

        private void OnEnable()
        {
            if(lookupCache == null)
                FillCache();
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)] 
        static void FillCache()
        {
            lookupCache ??= new();
            lookupCache.Clear();
            foreach (T obj in Resources.LoadAll<T>(""))
            {
                if (lookupCache.TryAdd(obj.Guid, obj)) continue;
                
                Debug.LogError($"Multiple objects sharing same GUID: {obj.Guid.ToString()}");
                Debug.LogError($"Object 1: {obj.name}", obj);
                Debug.LogError($"Object 2: {lookupCache[obj.Guid].name}",lookupCache[obj.Guid]);
            }
        }

        public bool Equals(T other)
        {

            if (other == null)
                return false;

            return Guid.Equals(other.Guid);
        }
        
        public static implicit operator SGuid(IdentifyableObject<T> @object) => @object.Guid;
    }

}