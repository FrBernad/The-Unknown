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

        public void Move(Vector3 direction)
        {
            float auxSpeed = _sprint ? speed * 2 : speed;
            transform.Translate(direction * (auxSpeed * Time.deltaTime));
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