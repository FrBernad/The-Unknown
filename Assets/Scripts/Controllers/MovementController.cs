using System;
using Entities;
using Interfaces;
using UnityEngine;


namespace Controllers
{
    public class MovementController : MonoBehaviour, IMovable
    {
        private const float _GRAVITY = -9.81f;

        public float Speed => GetComponent<Character>().CharacterStats.Speed;
        public float RotationSpeed => GetComponent<Character>().CharacterStats.RotationSpeed;

        private Boolean _sprint = false;

        private Rigidbody _rigidbody;
        private float JumpHeight => GetComponent<Character>().CharacterStats.JumpHeight;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        public void Move(Vector3 direction)
        {
            float realSpeed = _sprint ? Speed * 2 : Speed;
            transform.Translate(direction * (realSpeed * Time.deltaTime));
        }

        public void Jump(Vector3 direction)
        {
            float force = Mathf.Sqrt(JumpHeight * -2f * _GRAVITY);
            _rigidbody.AddForce(direction * force, ForceMode.Impulse);
        }

        public void Rotate(Vector3 direction)
        {
            transform.Rotate(direction * (RotationSpeed * Time.deltaTime));
        }

        public void Sprint(Boolean activateSprint)
        {
            _sprint = activateSprint;
        }
    }
}