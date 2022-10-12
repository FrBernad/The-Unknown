using System;
using Interfaces;
using UnityEngine;


namespace Controllers
{
    public class MovementController : MonoBehaviour, IMovable
    {
        public float Speed => speed;
        [SerializeField] private float speed = 10f;

        public float RotationSpeed => rotationSpeed;
        [SerializeField] private float rotationSpeed = 300f;

        private Boolean _sprint = false;

        private Rigidbody _rigidbody;

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
            _rigidbody.AddForce(direction, ForceMode.Impulse);
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