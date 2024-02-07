using System;
using UnityEngine;

namespace OctanGames
{
    public class Player : MonoBehaviour
    {
        [Header("Parameters")]
        [SerializeField] private float _moveSpeed = 7f;
        [SerializeField] private float _rotateSpeed = 10f;

        [Header("References")]
        [SerializeField] private GameInput _gameInput;

        private Vector3 _moveDirection;

        public bool IsWalking => _moveDirection != Vector3.zero;

        private void Update()
        {
            Vector2 inputVector = _gameInput.GetMovementVectorNormalized();
            _moveDirection = new Vector3(inputVector.x, 0f, inputVector.y);

            transform.position += _moveDirection * _moveSpeed * Time.deltaTime;
            transform.forward = Vector3.Slerp(transform.forward, _moveDirection, Time.deltaTime * _rotateSpeed);
        }
    }
}