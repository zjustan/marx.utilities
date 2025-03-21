using System;
using UnityEngine;

namespace Marx.Utilities
{
    public class StateMachine : State
    {
        [SerializeField] private State entryState;
        public State CurrentState { get; private set; }

        private readonly LookupTable<Type, State> stateCache = new();
        private State nextState;
        
        public void GotoState<TState>() where TState : State
        {
            GotoState(GetState<TState>());
        }

        public void GotoState(State targetState)
        {
            if (Active)
            {
                DeactivateCurrentState();
                CurrentState = targetState;
                CurrentState.Activate();
            }
            else
            {
                nextState = targetState;
            }
            
        }

        public void GotoNoState() => DeactivateCurrentState();

        protected override void OnActivate()
        {
            if (nextState != null)
            {
                CurrentState = nextState;
                nextState = null;
            }
            CurrentState.Activate();
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
            State[] foundSates = GetComponentsInChildren<State>();
            foreach (State state in foundSates)
            {
                if(state == this || state.Initialized) continue;
                state.Initialize(this);
                stateCache.Add(state.GetType(), state);
            }

            if (transform.parent.TryGetComponentInParent(out StateMachine parentStateMachine))
            {
                parentStateMachine.Awake();
            }
            else
            {
                Initialize(this);
                Activate();
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
        
        private void DeactivateCurrentState()
        {
            if (CurrentState != null)
                CurrentState.Deactivate();

            CurrentState = null;
        }
        
        private TState GetState<TState>() where TState : State
        {
            if (TryGetStateFromCache(out TState state)) return state;
            if (!gameObject.TryGetComponentInChildren(out state) || state.Initialized)
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