﻿using System;
using UnityEngine;

namespace OctanGames
{
    public class Player : MonoBehaviour
    {
        private const float PLAYER_RADIUS = 0.7f;
        private const float PLAYER_HEIGHT = 2f;

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

            bool canMove = CanMove(_moveDirection);

            if (!canMove)
            {
                Vector3 moveDirectionX = new Vector3(_moveDirection.x, 0, 0).normalized;
                canMove = CanMove(moveDirectionX);

                if (canMove)
                {
                    _moveDirection = moveDirectionX;
                }
                else
                {
                    Vector3 moveDirectionZ = new Vector3(0, 0, _moveDirection.z).normalized;
                    canMove = CanMove(moveDirectionZ);

                    if (canMove)
                    {
                        _moveDirection = moveDirectionZ;
                    }
                }
            }

            if (canMove)
            {
                transform.position += _moveDirection * _moveSpeed * Time.deltaTime;
            }
            transform.forward = Vector3.Slerp(transform.forward, _moveDirection, Time.deltaTime * _rotateSpeed);
        }

        private bool CanMove(Vector3 moveDirection)
        {
            float moveDistance = _moveSpeed * Time.deltaTime;
            Vector3 position = transform.position;

            bool canMove = !Physics.CapsuleCast(position,
                position + Vector3.up * PLAYER_HEIGHT,
                PLAYER_RADIUS,
                moveDirection,
                moveDistance);
            return canMove;
        }
    }
}