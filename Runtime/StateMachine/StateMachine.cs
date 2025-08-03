using System;
using UnityEngine;

namespace Marx.Utilities
{
    /// <summary>
    /// Represents a finite state machine that manages transitions between different states.
    /// </summary>
    /// <remarks>
    /// The StateMachine class inherits from the base State class and provides functionality
    /// to transition between states, activate and deactivate states, and manage the current state lifecycle.
    /// It is designed to be extended for specific use cases, such as game state management or player actions.
    /// </remarks>
    public class StateMachine : State
    {
        [SerializeField] private State entryState;

        /// <summary>
        /// Gets the current active state of the state machine.
        /// </summary>
        /// <remarks>
        /// This property holds a reference to the active state within the state machine.
        /// It can be updated during state transitions via the `GotoState` methods or within the activation lifecycle methods.
        /// If the state machine is not active or no state is currently active, the value of this property may be null.
        /// </remarks>
        public State CurrentState { get; private set; }

        private readonly LookupTable<Type, State> stateCache = new();
        private State nextState;

        /// <summary>
        /// Transitions the state machine to the specified state of type <typeparamref name="TState"/>.
        /// </summary>
        /// <typeparam name="TState">
        /// The type of the target state to transition to. This type must inherit from the <see cref="State"/> class.
        /// </typeparam>
        public void GotoState<TState>() where TState : State {
            GotoState(GetState<TState>());
        }

        /// <summary>
        /// Transitions the state machine to the specified target state.
        /// </summary>
        /// <param name="targetState">
        /// The target state to transition to. This state must inherit from the <see cref="State"/> class.
        /// </param>
        public void GotoState(State targetState) {
            if (Active) {
                DeactivateCurrentState();
                CurrentState = targetState;
                CurrentState.Activate();
            }
            else
            {
                nextState = targetState;
            }
            
        }

        /// <summary>
        /// Deactivates the current state of the state machine, leaving no active state.
        /// </summary>
        public void GotoNoState() => DeactivateCurrentState();

        protected override void OnActivate()
        {
            if (nextState != null)
            {
                CurrentState = nextState;
                nextState = null;
            }

            if (CurrentState != null) {
                CurrentState.Initialize(this);
                CurrentState.Activate();
            }
        }

        protected override void OnActiveUpdate()
        {            
            if(CurrentState != null)
                CurrentState.ActiveUpdate();
        }

        protected override void OnDeactivate()
        {
            CurrentState.Deactivate();
        }

        private void Awake()
        {
            if(Initialized)
                return;
            
            CurrentState = entryState;
            if (transform.parent.TryGetComponentInParent(out StateMachine parentStateMachine))
            {
                parentStateMachine.Awake();
            }
            else
            {
                Initialize(this);
                Activate();
            }
            
            State[] foundSates = GetComponentsInChildren<State>();
            foreach (State state in foundSates)
            {
                if(state == this || state.Initialized) continue;
                state.Initialize(this);
                stateCache.Add(state.GetType(), state);
            }

           
        }

        private void Update()
        {
            if(Active)
                ActiveUpdate();
        }

        private bool TryGetStateFromCache<TState>(out TState state) where TState : State
        {
            state = null;
            if (!stateCache.TryResolve(typeof(TState), out State possibleState)) return false;
            if (possibleState is not TState result) return false;
            state = result;
            return true;
        }
        
        private void DeactivateCurrentState() {
            var deactivatingState = CurrentState;
            CurrentState = null;
            if (deactivatingState != null)
                deactivatingState.Deactivate();
        }
        
        protected TState GetState<TState>() where TState : State
        {
            if (TryGetStateFromCache(out TState state)) return state;
            if (!gameObject.TryGetComponentInChildren(out state))
            {
                GameObject go = new (typeof(TState).Name);
                go.transform.parent = transform;
                state = go.AddComponent<TState>();
            }

            stateCache.Add(typeof(TState), state);
            state.Initialize(this);

            return state;
        }
    }
}