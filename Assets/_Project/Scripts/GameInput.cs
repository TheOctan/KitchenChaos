using System;
using UnityEngine;

namespace OctanGames
{
    public class GameInput : MonoBehaviour
    {
        private PlayerInputAction _playerInputAction;

        private void Awake()
        {
            _playerInputAction = new PlayerInputAction();
            _playerInputAction.Player.Enable();
        }

        public Vector2 GetMovementVectorNormalized()
        {
            var inputVector = _playerInputAction.Player.Move.ReadValue<Vector2>();
            return inputVector.normalized;
        }
    }
}