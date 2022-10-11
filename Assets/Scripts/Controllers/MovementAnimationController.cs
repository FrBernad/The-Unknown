using System;
using UnityEngine;

namespace Controllers
{
    public class MovementAnimationController : MonoBehaviour
    {
        private Animator _animator;
        private int _isWalkingHash;
        private int _isRunningHash;

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _isWalkingHash = Animator.StringToHash("isWalking");
            _isRunningHash = Animator.StringToHash("isRunning");
        }

        private void Update()
        {
            bool isWalking = _animator.GetBool(_isWalkingHash);
            bool isRunning = _animator.GetBool(_isRunningHash);

            bool forwardPressed = Input.GetKey("w");
            bool runningPressed = Input.GetKey("left shift");

            if (!isWalking && forwardPressed)
            {
                _animator.SetBool(_isWalkingHash, true);
            }

            if (isWalking && !forwardPressed)
            {
                _animator.SetBool(_isWalkingHash, false);
            }

            if (!isRunning && forwardPressed && runningPressed)
            {
                _animator.SetBool(_isRunningHash, true);
            }

            if (isRunning && (!forwardPressed || !runningPressed))
            {
                _animator.SetBool(_isRunningHash, false);
            }
        }
    }
}