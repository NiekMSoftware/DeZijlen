using UnityEngine;
using UnityEngine.InputSystem;

namespace DeZijlen.Player.Input
{
    public class HandleInput
    {
        // Input
        
        /** The MoveInput, returning as a Vector2. */
        public Vector2 MoveInput { get; private set; }
        
        /** Left Trigger input returns a floating point with a range from 0 to 1. */
        public float LeftTriggerInput { get; private set; }
        
        /** Right trigger input returns a floating point with a range from 0 to 1. */
        public float RightTriggerInput { get; private set; }

        // Input actions
        private PlayerInputActions _playerInputActions;
        private PlayerInputActions.PlayerActions _playerActions;
        private PlayerInputActions.UIActions _uiActions;

        public HandleInput()
        {
            Initialize();
        }
        
        /// <summary>
        /// Initializes the input system upon construction.
        /// </summary>
        private void Initialize()
        {
            /* Initialize the player input actions */
            _playerInputActions = new PlayerInputActions();
            _playerActions = _playerInputActions.Player;
            _uiActions = _playerInputActions.UI;
            
            _playerActions.Enable();
            _uiActions.Disable();
            
            /* Register events upon initialization */
            RegisterEvents();
        }

        /// <summary>
        /// Register events based on the Game Object's activity.
        /// </summary>
        /// <remarks>
        /// It's preferred that this method would get called once the object gets activated (if it was disabled).
        /// </remarks>
        public void RegisterEvents()
        {
            // Move
            _playerActions.Move.performed += HandleMoveInput;
            _playerActions.Move.canceled += HandleMoveInput;

            // Left hand
            _playerActions.LH_Grab.performed += HandleLeftTriggerInput;
            _playerActions.LH_Grab.canceled += HandleLeftTriggerInput;
            
            // Right hand
            _playerActions.RH_Grab.performed += HandleRightTriggerInput;
            _playerActions.RH_Grab.canceled += HandleRightTriggerInput;
        }

        /// <summary>
        /// Unregister any applied events.
        /// </summary>
        /// <remarks>
        /// This method must get called upon a OnDestroy() or OnDisable() call.
        /// </remarks>
        public void UnregisterEvents()
        {
            // Move
            _playerActions.Move.performed -= HandleMoveInput;
            _playerActions.Move.canceled -= HandleMoveInput;
            
            // Left hand
            _playerActions.LH_Grab.performed -= HandleLeftTriggerInput;
            _playerActions.LH_Grab.canceled -= HandleLeftTriggerInput;
            
            // Right hand
            _playerActions.RH_Grab.performed -= HandleRightTriggerInput;
            _playerActions.RH_Grab.canceled -= HandleRightTriggerInput;
        }

        /** Reads out the move input and modifies the MoveInput property. */
        private void HandleMoveInput(InputAction.CallbackContext ctx)
        {
            // Modify move input property
            MoveInput = ctx.ReadValue<Vector2>();
        }

        /** Reads out the left trigger's input and modifies the LeftTriggerInput property. */
        private void HandleLeftTriggerInput(InputAction.CallbackContext ctx)
        {
            // Modify left trigger input
            LeftTriggerInput = ctx.ReadValue<float>();
        }

        /** Reads out the right trigger's input and modifies the RightTriggerInput property. */
        private void HandleRightTriggerInput(InputAction.CallbackContext ctx)
        {
            // Modify right trigger input
            RightTriggerInput = ctx.ReadValue<float>();
        }
    }
}
