using UnityEngine;

namespace Marx.Utilities
{

    /// <summary>
    /// Represents an abstract base class for creating states in a state machine.
    /// </summary>
    /// <remarks>
    /// A state defines specific behavior and can transition between other states within a state machine.
    /// This class provides methods and properties to manage the lifecycle and functionality of a state,
    /// including initialization, activation, deactivation, and updates.
    /// </remarks>
    public abstract class State : MonoBehaviour
    {
        /// <summary>
        /// Represents the state machine associated with the current state.
        /// </summary>
        /// <remarks>
        /// The state machine manages the transitions and behavior between different states.
        /// This property provides a reference to the state machine instance that the current state belongs to,
        /// allowing the state to query or interact with it during its lifecycle.
        /// </remarks>
        /// <value>
        /// Returns the <see cref="StateMachine"/> instance to which the current state is associated.
        /// </value>
        public StateMachine StateMachine { get; private set; }

        /// <summary>
        /// Indicates whether the state has been initialized within the state machine.
        /// </summary>
        /// <remarks>
        /// A state is considered initialized after it has been set up with its associated state machine.
        /// Initialization is typically performed only once during the lifecycle of the state
        /// to ensure proper configuration and avoid redundant operations.
        /// </remarks>
        /// <value>
        /// Returns <c>true</c> if the state is initialized; otherwise, <c>false</c>.
        /// </value>
        public bool Initialized { get; private set; }

        /// <summary>
        /// Indicates whether the current state is active in the state machine.
        /// </summary>
        /// <remarks>
        /// A state is considered active when the state machine is either directly set to this state
        /// or when the state machine itself is active and the current state matches this state.
        /// </remarks>
        /// <value>
        /// Returns <c>true</c> if the state is active; otherwise, <c>false</c>.
        /// </value>
        public bool Active => StateMachine == this || (StateMachine.Active && StateMachine.CurrentState == this) ;

        /// <summary>
        /// Initializes the state and associates it with the specified state machine.
        /// </summary>
        /// <remarks>
        /// This method is responsible for setting up the state by establishing its relationship
        /// with the given state machine and marking it as initialized. It ensures that the
        /// initialization logic is executed only once and invokes the <see cref="OnInitialized"/> method
        /// for additional setup in derived classes.
        /// </remarks>
        /// <param name="stateMachine">The <see cref="StateMachine"/> instance to associate with this state.</param>
        public void Initialize(StateMachine stateMachine) {
            if (Initialized)
                return;

            Initialized = true;
            StateMachine = stateMachine;
            OnInitialized();
        }


        /// <summary>
        /// Activates the state within the state machine and triggers the state's activation logic.
        /// </summary>
        /// <remarks>
        /// This method is used to switch the state to an active state and prepare it for execution.
        /// It internally invokes the <see cref="OnActivate"/> method, allowing derived classes to implement
        /// specific activation logic.
        /// </remarks>
        /// <example>
        /// This method is typically called as part of the state machine's lifecycle when transitioning
        /// to this state. It ensures the state is properly set up and ready for active operation.
        /// </example>
        public void Activate() => OnActivate();

        /// <summary>
        /// Called when the state is activated within the state machine.
        /// </summary>
        /// <remarks>
        /// This method is invoked as part of the state's lifecycle, typically when the state transitions
        /// from an inactive to an active state. Override this method in a derived class to implement
        /// custom activation logic specific to the state.
        /// </remarks>
        /// <example>
        /// This method might perform tasks such as initializing parameters, updating visuals,
        /// or preparing objects for use in the active state.
        /// </example>
        protected virtual void OnActivate() {  }

        /// <summary>
        /// Performs the active update logic for the current state.
        /// </summary>
        /// <remarks>
        /// This method is invoked during the update cycle when the state is active. It calls the
        /// <see cref="OnActiveUpdate"/> method, allowing derived classes to implement custom
        /// behavior specific to the active state.
        /// </remarks>
        public void ActiveUpdate() => OnActiveUpdate();

        /// <summary>
        /// Executes the active update logic specific to the current state.
        /// </summary>
        /// <remarks>
        /// This method is called during the state machine's update cycle to perform actions or
        /// behaviors specific to the state. Derived classes override this method to implement
        /// their own state-specific update logic, such as handling animations, physics, or input
        /// processing. If overridden, be sure to call the base implementation if necessary.
        /// </remarks>
        protected virtual void OnActiveUpdate() {  }

        /// <summary>
        /// Deactivates the current state.
        /// </summary>
        /// <remarks>
        /// This method handles the deactivation logic for the state by invoking
        /// the <see cref="OnDeactivate"/> method, which can be overridden
        /// in derived classes to implement custom deactivation behavior. It ensures that
        /// the state transitions out of its active state and allows for cleanup
        /// or other specific logic to occur during deactivation.
        /// </remarks>
        public void Deactivate() => OnDeactivate();

        /// <summary>
        /// Handles the deactivation logic for the state when transitioning out of its active state.
        /// </summary>
        /// <remarks>
        /// This method is invoked when the state is being deactivated. It allows derived classes
        /// to implement custom cleanup or state-specific logic upon deactivation. The default
        /// implementation does nothing but can be overridden in subclasses to perform additional operations.
        /// </remarks>
        protected virtual void OnDeactivate() { }
        /// <summary>
        /// 
        /// </summary>
        protected virtual void OnInitialized() {  }

        /// <summary>
        /// Renames the current game object to match the name of the state class.
        /// </summary>
        /// <remarks>
        /// This method is intended to enhance clarity in the Unity Editor by synchronizing
        /// the game object's name with the specific state class it represents. This can be
        /// especially helpful for debugging and identifying states during development.
        /// </remarks>

#if UNITY_EDITOR
        [ContextMenu("Rename game object to state name")]
        private void RenameGameObjToState() {
            gameObject.name = GetType().Name;
        }
        
        #endif
    }
}