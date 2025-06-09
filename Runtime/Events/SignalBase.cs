using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace Marx.Utilities {
    /// <summary>
    /// Represents a base signal that implements the base functionality for
    /// the publish-subscribe pattern in a generic and reusable way.
    /// </summary>
    public abstract class SignalBase {

        private List<SignalListener> listeners = new();

        protected void Invoke(params object[] args) {
            foreach (SignalListener listener in listeners) {
                listener.TryInvoke(args);
            }
        }

        protected IDisposable AddListener(SignalListener listener) {
            listeners.DistinctAdd(listener);
            return listener;
        }

        protected void RemoveListener(SignalListener listener) {
            listeners.Remove(listener);
        }

        protected class SignalListener : IDisposable {
            private readonly Action<object[]> executor;
            private readonly MethodInfo method;
            private readonly object target;
            private readonly SignalBase signal;

            public SignalListener(SignalBase signal, object target, MethodInfo method, Action<object[]> executor) {
                this.signal = signal;
                this.target = target;
                this.method = method;
                this.executor = executor;
            }

            public void TryInvoke(object[] args) {
                try {
                    executor?.Invoke(args);
                } catch (Exception e) {
                    Debug.LogException(e);
                }
            }

            public bool HasSameTarget(object otherTarget, MethodInfo otherMethod) =>
                target == otherTarget && method == otherMethod;

            public void Dispose() {
                signal.RemoveListener(this);
            }

        }

        protected void TryRemoveListener(object target, MethodInfo methodInfo) {
            listeners.RemoveAll(x => x.HasSameTarget(target, methodInfo));
        }

    }

}