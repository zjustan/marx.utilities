using System;
using System.Collections.Generic;

namespace Marx.Utilities
{
    /// <summary>
    /// Represents a collection that manages subscriptions to signals and their corresponding listeners.
    /// This class provides a mechanism to group listeners together and manage them in bulk.
    /// </summary>
    public class ObservableCollection 
    {
        private List<IDisposable> listeners = new List<IDisposable>();

        /// <summary>
        /// Adds a listener for the specified signal and associates it with the provided action.
        /// </summary>
        /// <param name="signal">The signal to listen to. This is the event that triggers the associated action.</param>
        /// <param name="action">The action to execute when the signal is triggered.</param>
        public void AddListener(Signal signal, Action action) {
            listeners.Add(signal.AddListener(action));
        }
        
        /// <summary>
        /// Adds a listener for the specified signal and associates it with the provided action.
        /// </summary>
        /// <param name="signal">The signal to listen to. This is the event that triggers the associated action.</param>
        /// <param name="action">The action to execute when the signal is triggered.</param>
        public void AddListener<T1>(Signal<T1> signal, Action<T1> action)
        {
            listeners.Add(signal.AddListener(action));
        }
        
        /// <summary>
        /// Adds a listener for the specified signal and associates it with the provided action.
        /// </summary>
        /// <param name="signal">The signal to listen to. This is the event that triggers the associated action.</param>
        /// <param name="action">The action to execute when the signal is triggered.</param>
        public void AddListener<T1, T2>(Signal<T1, T2> signal, Action<T1, T2> action)
        {
            listeners.Add(signal.AddListener(action));
        }
        
        /// <summary>
        /// Adds a listener for the specified signal and associates it with the provided action.
        /// </summary>
        /// <param name="signal">The signal to listen to. This is the event that triggers the associated action.</param>
        /// <param name="action">The action to execute when the signal is triggered.</param>        
        public void AddListener<T1, T2, T3>(Signal<T1, T2, T3> signal, Action<T1, T2, T3> action)
        {
            listeners.Add(signal.AddListener(action));
        }
        
        /// <summary>
        /// Adds a listener for the specified signal and associates it with the provided action.
        /// </summary>
        /// <param name="signal">The signal to listen to. This is the event that triggers the associated action.</param>
        /// <param name="action">The action to execute when the signal is triggered.</param>
        public void AddListener<T1, T2, T3, T4>(Signal<T1, T2, T3, T4> signal, Action<T1, T2, T3, T4> action)
        {
            listeners.Add(signal.AddListener(action));
        }

        /// <summary>
        /// Removes all registered listeners from the collection and disposes of their subscriptions.
        /// </summary>
        public void RemoveAllListeners() {
            int i = listeners.Count;
            while (--i > -1)
            {
                listeners[i].Dispose();
                listeners.RemoveAt(i);
            }
        }
    }
}
