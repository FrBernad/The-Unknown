using System;
using Interfaces;
using UnityEngine;


namespace Controllers
{
    public class MovementController : MonoBehaviour, IMovable
    {
        private const float _GRAVITY = -9.81f;

        public float Speed => speed;
        [SerializeField] private float speed = 10f;
        public float RotationSpeed => rotationSpeed;
        [SerializeField] private float rotationSpeed = 300f;

        private Boolean _sprint = false;

        private Rigidbody _rigidbody;
        [SerializeField] private float jumpHeight = 2f;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        public void Move(Vector3 direction)
        {
            float realSpeed = _sprint ? speed * 2 : speed;
            transform.Translate(direction * (realSpeed * Time.deltaTime));
        }

        public void Jump(Vector3 direction)
        {
            float force = Mathf.Sqrt(jumpHeight * -2f * _GRAVITY);
            _rigidbody.AddForce(direction * force, ForceMode.Impulse);
        }

        public void Rotate(Vector3 direction)
        {
            transform.Rotate(direction * (rotationSpeed * Time.deltaTime));
        }

        public void Sprint(Boolean activateSprint)
        {
            _sprint = activateSprint;
        }
    }
}