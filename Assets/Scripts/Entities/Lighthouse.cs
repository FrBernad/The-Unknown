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

        [SerializeField] private float _targetMinDistance = 50;
        [SerializeField] private float _targetMaxDistance = 100;

        [SerializeField] private AudioSource _audioSource;

        private RotationMode _rotationMode = RotationMode.Rotate;

        private void Update()
        {
            switch (_rotationMode)
            {
                case RotationMode.Rotate:
                case RotationMode.Target when !PlayerInRange():
                    if (_audioSource.isPlaying) _audioSource.Stop();
                    NormalRotation();
                    break;
                case RotationMode.Target:
                    if (!_audioSource.isPlaying) _audioSource.Play();
                    TargetPlayer();
                    break;
            }
        }

        private void NormalRotation()
        {
            // SpotLight
            _spotLight.transform.Rotate(Vector3.up * (Rad2Deg * (Time.deltaTime * _speed)));
            var spotlightLocalRotation = _spotLight.transform.localRotation;
            _spotLight.transform.localRotation =
                new Quaternion(0, spotlightLocalRotation.y, 0, spotlightLocalRotation.w);

            // PointLight
            var pointLightPosition = _pointLight.transform.localPosition;

            var rotation = spotlightLocalRotation.eulerAngles.y - _offset;
            var x = Cos(-Deg2Rad * rotation) * _radius;
            var y = pointLightPosition.y;
            var z = Sin(-Deg2Rad * rotation) * _radius;

            _pointLight.transform.localPosition = new Vector3(x, y, z);
        }

        private void TargetPlayer()
        {
            var direction = _character.transform.position - _spotLight.transform.position;
            var targetRotation = Quaternion.LookRotation(direction);

            // SpotLight
            var spotlightTargetRotation = _spotLight.transform.rotation;
            spotlightTargetRotation = Quaternion.Slerp(spotlightTargetRotation, targetRotation, Time.deltaTime);
            _spotLight.transform.rotation = spotlightTargetRotation;

            // PointLight
            var pointLightPosition = _pointLight.transform.localPosition;

            var rotation = _spotLight.transform.localRotation.eulerAngles.y - _offset;
            var x = Cos(-Deg2Rad * rotation) * _radius;
            var y = pointLightPosition.y;
            var z = Sin(-Deg2Rad * rotation) * _radius;
            _pointLight.transform.localPosition = new Vector3(x, y, z);
        }

        private bool PlayerInRange()
        {
            var distance = (_character.transform.position - _spotLight.transform.position).magnitude;
            return distance > _targetMinDistance && distance < _targetMaxDistance;
        }

        public void SetRotationMode(RotationMode rotationMode)
        {
            _rotationMode = rotationMode;
        }

        public enum RotationMode
        {
            Rotate,
            Target
        }
    }
}