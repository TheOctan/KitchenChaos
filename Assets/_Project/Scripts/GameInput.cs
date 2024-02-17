using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace OctanGames
{
    public class GameInput : MonoBehaviour
    {
        public event Action InteractionPressed;

        private PlayerInputAction _playerInputAction;

        private void Awake()
        {
            _playerInputAction = new PlayerInputAction();
            _playerInputAction.Player.Enable();
            
            _playerInputAction.Player.Interact.performed += OnInteractPerformed;
        }

        private void OnInteractPerformed(InputAction.CallbackContext args)
        {
            InteractionPressed?.Invoke();
        }

        public Vector2 GetMovementVectorNormalized()
        {
            var inputVector = _playerInputAction.Player.Move.ReadValue<Vector2>();
            return inputVector.normalized;
        }
    }
}