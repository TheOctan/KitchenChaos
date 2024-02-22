using System;
using UnityEngine;

namespace OctanGames
{
    public class Player : MonoBehaviour
    {
        private const float PLAYER_RADIUS = 0.7f;
        private const float PLAYER_HEIGHT = 2f;
        private const float INTERACT_DISTANCE = 2f;

        public event Action<ClearCounter> SelectedCounterChanged;

        [Header("Parameters")]
        [SerializeField] private float _moveSpeed = 7f;
        [SerializeField] private float _rotateSpeed = 10f;
        [SerializeField] private LayerMask _countersLayerMask;

        [Header("References")]
        [SerializeField] private GameInput _gameInput;

        private Vector3 _moveDirection;
        private Vector3 _lastInteractDirection;
        private ClearCounter _selectedCounter;

        public static Player Instance { get; private set; }
        public bool IsWalking => _moveDirection != Vector3.zero;
        private ClearCounter SelectedCounter
        {
            get => _selectedCounter;
            set
            {
                _selectedCounter = value;
                SelectedCounterChanged?.Invoke(_selectedCounter);
            }
        }

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("There is more than one Player instance");
            }
            Instance = this;
        }

        private void Start()
        {
            _gameInput.InteractionPressed += OnInteractionPressed;
        }

        private void Update()
        {
            Vector2 inputVector = _gameInput.GetMovementVectorNormalized();
            _moveDirection = new Vector3(inputVector.x, 0f, inputVector.y);

            HandleMovement();
            HandleInteractions();
        }

        private void OnDestroy()
        {
            _gameInput.InteractionPressed -= OnInteractionPressed;
        }

        private void OnInteractionPressed()
        {
            if (_selectedCounter != null)
            {
                _selectedCounter.Interact();
            }
        }

        private void HandleInteractions()
        {
            Vector2 inputVector = _gameInput.GetMovementVectorNormalized();
            _moveDirection = new Vector3(inputVector.x, 0f, inputVector.y);

            if (_moveDirection != Vector3.zero)
            {
                _lastInteractDirection = _moveDirection;
            }

            if (Physics.Raycast(transform.position, _lastInteractDirection, out RaycastHit raycastHit,
                    INTERACT_DISTANCE, _countersLayerMask.value)
                && raycastHit.transform.TryGetComponent(out ClearCounter clearCounter))
            {
                if (clearCounter != null && clearCounter != SelectedCounter)
                {
                    SelectedCounter = clearCounter;
                }
            }
            else
            {
                SelectedCounter = null;
            }
        }

        private void HandleMovement()
        {
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
                transform.position += _moveSpeed * Time.deltaTime * _moveDirection;
            }

            if (_moveDirection != Vector3.zero)
            {
                transform.forward = Vector3.Slerp(transform.forward, _moveDirection, Time.deltaTime * _rotateSpeed);
            }
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