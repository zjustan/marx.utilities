using System.Collections;
using System.Collections.Generic;

namespace Marx.Utilities
{

    /// <summary>
    /// A collection that provides functionality for constructing and managing multiple objects,
    /// while also facilitating cleanup operations.
    /// </summary>
    public class FactoryCollection : IEnumerable<object>
    {
        private List<object> objects = new();

        public IEnumerator<object> GetEnumerator()
        {
            return objects.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return objects.GetEnumerator();
        }

        public T Construct<T>(params object[] parameters)
        {
            T result = Factories.Construct<T>(parameters);
            objects.Add(result);
            return result;
        }

        public void Clean()
        {
            Cleaners.Clean(this);
            objects.Clear();
        }

        internal List<T> ConstructMultiple<T>(int count, params object[] parameters)
        {
            List<T> result = Factories.ConstructMultiple<T>(count, parameters);
            objects.AddRange(result as IEnumerable<object>);
            return result;
        }
    }

}