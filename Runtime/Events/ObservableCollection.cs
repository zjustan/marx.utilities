using System;
using System.Collections.Generic;

namespace Marx.Utilities
{
    public class ObservableCollection 
    {
        private List<IDisposable> listeners = new List<IDisposable>();

        public void AddListener(Signal signal, Action action)
        {
            listeners.Add(signal.AddListener(action));
        }
        
        public void AddListener<T1>(Signal<T1> signal, Action<T1> action)
        {
            listeners.Add(signal.AddListener(action));
        }
        
        public void AddListener<T1, T2>(Signal<T1, T2> signal, Action<T1, T2> action)
        {
            listeners.Add(signal.AddListener(action));
        }
        
               
        public void AddListener<T1, T2, T3>(Signal<T1, T2, T3> signal, Action<T1, T2, T3> action)
        {
            listeners.Add(signal.AddListener(action));
        }
        
        public void AddListener<T1, T2, T3, T4>(Signal<T1, T2, T3, T4> signal, Action<T1, T2, T3, T4> action)
        {
            listeners.Add(signal.AddListener(action));
        }

        public void RemoveAllListeners()
        {
            int i = listeners.Count;
            while (--i > -1)
            {
                listeners[i].Dispose();
                listeners.RemoveAt(i);
            }
        }
    }
}
