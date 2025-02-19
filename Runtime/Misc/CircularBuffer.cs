using System.Collections;
using System.Collections.Generic;

namespace Marx.Utilities 
{
    public class CircularBuffer<T> : IEnumerable<T>
    {
        private T[] buffer;
        int current;
        int capacity;

        public CircularBuffer(int capacity)
        {
            this.capacity = capacity;
            buffer = new T[capacity];
        }

        public void Append(T item)
        {
            Increment(ref current);
            buffer[current] = item;
        }

        private void Increment(ref int index)
        {
            if (++index == capacity)
                index = 0;
        }

        public IEnumerator<T> GetEnumerator() => GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => buffer.GetEnumerator();
    }
}
