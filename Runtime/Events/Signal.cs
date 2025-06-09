using System;

namespace Marx.Utilities {

    /// <summary>
    /// Represents a signal that implements the base functionality for
    /// the publish-subscribe pattern in a generic and reusable way.
    /// </summary>
    public class Signal : SignalBase {
        public void Invoke() {
            base.Invoke();
        }

        public IDisposable AddListener(Action listener) {
            return base.AddListener(new SignalListener(this, listener.Target, listener.Method, x => listener.Invoke()));
        }

        public void RemoveListener(Action listener) {
            TryRemoveListener(listener.Target, listener.Method);
        }
    }

    /// <summary>
    /// Represents a signal that implements the base functionality for
    /// the publish-subscribe pattern in a generic and reusable way.
    /// </summary>
    public class Signal<T1> : SignalBase {

        public void Invoke(T1 arg1) {
            base.Invoke(arg1);
        }

        public IDisposable AddListener(Action<T1> listener) {
            Action<object[]> executor = x => listener.Invoke((T1)x[0]);
            return base.AddListener(new SignalListener(this, listener.Target, listener.Method, executor));
        }

        void RemoveListener(Action<T1> listener) {
            TryRemoveListener(listener.Target, listener.Method);
        }
    }

    /// <summary>
    /// Represents a signal that implements the base functionality for
    /// the publish-subscribe pattern in a generic and reusable way.
    /// </summary>
    public class Signal<T1, T2> : SignalBase {

        public void Invoke(T1 arg1, T2 arg2) {
            base.Invoke(arg1, arg2);
        }

        public IDisposable AddListener(Action<T1, T2> listener) {
            Action<object[]> executor = (x) => listener.Invoke((T1)x[0], (T2)x[1]);
            return base.AddListener(new SignalListener(this, listener.Target, listener.Method, executor));
        }

        void RemoveListener(Action<T1, T2> listener) {
            TryRemoveListener(listener.Target, listener.Method);
        }
    }

    /// <summary>
    /// Represents a signal that implements the base functionality for
    /// the publish-subscribe pattern in a generic and reusable way.
    /// </summary>
    public class Signal<T1, T2, T3> : SignalBase {

        public void Invoke(T1 arg1, T2 arg2, T3 arg3) {
            base.Invoke(arg1, arg2, arg3);
        }

        public IDisposable AddListener(Action<T1, T2, T3> listener) {
            Action<object[]> executor = x => listener.Invoke((T1)x[0], (T2)x[1], (T3)x[2]);
            return base.AddListener(new SignalListener(this, listener.Target, listener.Method, executor));
        }

        void RemoveListener(Action<T1, T2, T3> listener) {
            TryRemoveListener(listener.Target, listener.Method);
        }
    }

    /// <summary>
    /// Represents a signal that implements the base functionality for
    /// the publish-subscribe pattern in a generic and reusable way.
    /// </summary>
    public class Signal<T1, T2, T3, T4> : SignalBase {

        public void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4) {
            base.Invoke(arg1, arg2, arg3);
        }

        public IDisposable AddListener(Action<T1, T2, T3, T4> listener) {
            Action<object[]> executor = x => listener.Invoke((T1)x[0], (T2)x[1], (T3)x[2], (T4)x[3]);
            return base.AddListener(new SignalListener(this, listener.Target, listener.Method, executor));
        }

        void RemoveListener(Action<T1, T2, T3, T4> listener) {
            TryRemoveListener(listener.Target, listener.Method);
        }
    }
}