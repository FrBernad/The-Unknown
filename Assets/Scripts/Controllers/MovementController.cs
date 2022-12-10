using Entities;
using Strategy;
using UnityEngine;

namespace Controllers
{
    public class MovementController : MonoBehaviour, IMovable
    {
        private const float _GRAVITY = -9.81f;

        private Rigidbody _rigidbody;
        private float JumpHeight => GetComponent<Character>().CharacterStats.JumpHeight;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        public float RotationSpeed => GetComponent<Character>().CharacterStats.RotationSpeed;
        public float Speed => GetComponent<Character>().CharacterStats.Speed;
        private float SlowedSpeed => GetComponent<Character>().CharacterStats.SlowedSpeed;

        public bool Sprinting { get; private set; }

        private bool Slowed { get; set; }

        public void Move(Vector3 direction)
        {
            var realSpeed = Sprinting ? Speed * 2 : Speed;
            if (Slowed) realSpeed = Sprinting ? SlowedSpeed * 2 : SlowedSpeed;
            transform.Translate(direction * (realSpeed * Time.deltaTime));
        }

        public void Jump(Vector3 direction)
        {
            var force = Mathf.Sqrt(JumpHeight * -2f * _GRAVITY);
            _rigidbody.AddForce(direction * force, ForceMode.Impulse);
        }

        public void Rotate(Vector3 direction)
        {
            transform.Rotate(direction * (RotationSpeed * Time.deltaTime));
        }

        public void Sprint(bool isSprinting)
        {
            Sprinting = isSprinting;
        }

        public void Slow(bool isSlowed)
        {
            Slowed = isSlowed;
        }
    }
}