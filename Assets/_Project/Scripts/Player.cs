using System;
using UnityEngine;

namespace OctanGames
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed = 7f;
        [SerializeField] private float _rotateSpeed = 10f;

        private void Update()
        {
            var inputVector = new Vector2();

            if (Input.GetKey(KeyCode.W))
            {
                inputVector.y += 1;
            }
            if (Input.GetKey(KeyCode.S))
            {
                inputVector.y -= 1;
            }
            if (Input.GetKey(KeyCode.A))
            {
                inputVector.x -= 1;
            }
            if (Input.GetKey(KeyCode.D))
            {
                inputVector.x += 1;
            }

            Vector3 moveDirection = new Vector3(inputVector.x, 0f, inputVector.y).normalized;
            transform.position += moveDirection * _moveSpeed * Time.deltaTime;
            transform.forward = Vector3.Slerp(transform.forward, moveDirection, Time.deltaTime * _rotateSpeed);
        }
    }
}