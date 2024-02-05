using System;
using UnityEngine;

namespace OctanGames
{
    public class PlayerAnimator : MonoBehaviour
    {
        private const string IS_WALKING = "IsWalking";
        private static readonly int _isWalking = Animator.StringToHash(IS_WALKING);

        [SerializeField] private Animator _animator;
        [SerializeField] private Player _player;

        private void Update()
        {
            _animator.SetBool(_isWalking, _player.IsWalking);
        }
    }
}