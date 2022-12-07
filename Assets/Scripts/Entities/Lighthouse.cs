using System;
using UnityEngine;
using UnityEngine.Scripting;
using static System.Math;
using static UnityEngine.Mathf;
using Object = UnityEngine.Object;

namespace Entities
{
    public class Lighthouse : MonoBehaviour
    {
        [SerializeField] private GameObject _pointLight;
        [SerializeField] private GameObject _spotLight;
        [SerializeField] private Character _character;

        [SerializeField] private float _speed = 0.1F;
        [SerializeField] private float _radius = 3;
        [SerializeField] private float _offset = 90;

        private float _currentTime;

        private void Update()
        {
            TargetPlayer();
            // NormalRotation();
        }

        private void NormalRotation()
        {
            _spotLight.transform.Rotate(Vector3.up * Rad2Deg * (Time.deltaTime * _speed));

            var pointLightPosition = _pointLight.transform.localPosition;

            var rotation = _spotLight.transform.localRotation.eulerAngles.y - _offset;
            var x = Cos(-Deg2Rad * rotation) * _radius;
            var y = pointLightPosition.y;
            var z = Sin(-Deg2Rad * rotation) * _radius;

            _pointLight.transform.localPosition = new Vector3(x, y, z);
        }

        private void TargetPlayer()
        {
            var direction = _character.transform.position - _spotLight.transform.position;
            var targetRotation = Quaternion.LookRotation(direction);
            var spotlightTargetRotation = _spotLight.transform.rotation;
            spotlightTargetRotation = Quaternion.Slerp(spotlightTargetRotation, targetRotation, Time.deltaTime);
            _spotLight.transform.rotation = spotlightTargetRotation;

            var rotation = _spotLight.transform.localRotation.eulerAngles.y - _offset;
            var pointLightPosition = _pointLight.transform.localPosition;
            var x = Cos(-Deg2Rad * rotation) * _radius;
            var y = pointLightPosition.y;
            var z = Sin(-Deg2Rad * rotation) * _radius;
            _pointLight.transform.localPosition = new Vector3(x, y, z);
        }
    }
}