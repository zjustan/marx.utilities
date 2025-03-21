using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

namespace Marx.Utilities
{
    public abstract class State : MonoBehaviour
    {
        public StateMachine StateMachine { get; private set; }
        public bool Initialized { get; private set; }
        public bool Active => StateMachine == this || (StateMachine.Active && StateMachine.CurrentState == this) ;

        public void Initialize(StateMachine stateMachine)
        {
            if (Initialized)
                return;

            Initialized = true;
            StateMachine = stateMachine;
        }


        public void Activate() => OnActivate();
        protected virtual void OnActivate() {  }

        public void ActiveUpdate() => OnActiveUpdate();

        protected virtual void OnActiveUpdate() {  }
        public void Deactivate() => OnDeactivate();
        protected virtual void OnDeactivate() { }
    }
}