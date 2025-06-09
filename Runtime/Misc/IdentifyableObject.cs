using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Marx.Utilities
{

    /// <summary>
    /// Represents a base class for objects that have a unique identifier and can be used in various systems.
    /// This class provides functionality for managing and accessing instances of derived objects using a globally unique identifier.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the derived class inheriting from IdentifyableObject.
    /// This ensures type-safe usage and enables derived classes to use type-specific functionality.
    /// </typeparam>
    public class IdentifyableObject<T> : ScriptableObject, IEquatable<T> where T : IdentifyableObject<T>
    {
        /// <summary>
        /// A uniquely identifiable property of type <see cref="SGuid"/> that is used
        /// to represent the globally unique identifier (GUID) for instances of the
        /// object. This property ensures that each instance possesses a distinct
        /// identifier that is assigned and serialized as part of the object.
        /// </summary>
        /// <remarks>
        /// This field is private by default and serialized in Unity; it is exposed
        /// as a publicly accessible property with a private setter. It is utilized
        /// to uniquely identify objects in various systems and contexts.
        /// </remarks>
        /// <value>
        /// The unique <see cref="SGuid"/> that represents the identifier of the object.
        /// </value>
        /// <example>
        /// The <see cref="Guid"/> can be used to fetch or compare instances
        /// of the object within a collection or during runtime object lookups.
        /// </example>
        [field: SerializeField]
        public SGuid Guid { get; private set; }

        /// <summary>
        /// Retrieves an instance of the specified type associated with the given unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the object to retrieve.</param>
        /// <returns>
        /// An instance of the specified type associated with the provided identifier,
        /// or <c>null</c> if no instance is found.
        /// </returns>
        public static T Get(SGuid id) {
            Lookup.TryGetValue(id, out var result);
            return result;
        }

        /// <summary>
        /// Retrieves a read-only collection of all instances of the derived objects of type <see cref="IdentifyableObject{T}"/>.
        /// </summary>
        /// <remarks>
        /// This property provides access to all instantiated objects of the specified type.
        /// The instance collection is maintained and populated internally to ensure that all objects are properly registered.
        /// Accessing this property will ensure the cache is up-to-date by invoking the necessary fill mechanism.
        /// </remarks>
        /// <value>
        /// An <see cref="IReadOnlyCollection{T}"/> containing all instances of the derived object type <typeparamref name="T"/>.
        /// </value>
        public static IReadOnlyCollection<T> All
        {
            get
            {
                FillCache();
                return lookupCache.Values;
            }
        }

        /// <summary>
        /// Provides a read-only collection of all objects of type <typeparamref name="T"/>
        /// organized in a dictionary where the key is the unique identifier <see cref="SGuid"/>.
        /// This property enables efficient runtime lookups of objects by their identifiers.
        /// </summary>
        /// <remarks>
        /// The dictionary is cached to ensure fast access and is populated automatically
        /// at runtime with the instances of <typeparamref name="T"/>.
        /// The cache is filled during the initialization phase to ensure availability of objects
        /// before they are accessed.
        /// </remarks>
        /// <value>
        /// An <see cref="IReadOnlyDictionary{SGuid, T}"/> containing the mapping of
        /// unique identifiers to their corresponding instances. Ensures that all objects
        /// of type <typeparamref name="T"/> are accessible through their <see cref="SGuid"/>.
        /// </value>
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
        private static void FillCache()
        {
            lookupCache ??= new();
            lookupCache.Clear();
            foreach (T obj in Resources.LoadAll<T>(""))
            {
                if (lookupCache.TryAdd(obj.Guid, obj)) continue;
                if(lookupCache[obj.Guid] == obj) continue;
                
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