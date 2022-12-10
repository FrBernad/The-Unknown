using Managers;
using UnityEngine;
using static UnityEngine.Mathf;

namespace Entities.Lighthouse
{
    public class Lighthouse : MonoBehaviour
    {
        [SerializeField] private GameObject _pointLight;
        [SerializeField] private GameObject _spotLight;
        [SerializeField] private SlowTrigger _slowTrigger;
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
                    if (_slowTrigger.gameObject.activeSelf)
                    {
                        _slowTrigger.gameObject.SetActive(false);
                        if (_slowTrigger.isTriggered) EventManager.instance.SlowPlayer(false);
                    }

                    NormalRotation();
                    break;
                case RotationMode.Target:
                    if (!_audioSource.isPlaying) _audioSource.Play();
                    if (!_slowTrigger.gameObject.activeSelf) _slowTrigger.gameObject.SetActive(true);
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

            var pointLightRotation = spotlightLocalRotation.eulerAngles.y - _offset;
            var pointLightX = Cos(-Deg2Rad * pointLightRotation) * _radius;
            var pointLightY = pointLightPosition.y;
            var pointLightZ = Sin(-Deg2Rad * pointLightRotation) * _radius;

            _pointLight.transform.localPosition = new Vector3(pointLightX, pointLightY, pointLightZ);

            // Slow Trigger
            _slowTrigger.transform.Rotate(Vector3.up * (Rad2Deg * (Time.deltaTime * _speed)));
            var slowTriggerLocalRotation = _slowTrigger.transform.localRotation;
            _slowTrigger.transform.localRotation =
                new Quaternion(0, slowTriggerLocalRotation.y, 0, slowTriggerLocalRotation.w);
        }

        private void TargetPlayer()
        {
            var targetPosition = _character.transform.position;
            var direction = targetPosition - _spotLight.transform.position;
            var targetRotation = Quaternion.LookRotation(direction);

            // SpotLight
            var spotLightLocalRotation = _spotLight.transform.localRotation;
            var spotlightTargetRotation = _spotLight.transform.rotation;
            spotlightTargetRotation = Quaternion.Slerp(spotlightTargetRotation, targetRotation, Time.deltaTime);
            _spotLight.transform.rotation = spotlightTargetRotation;

            // PointLight
            var pointLightPosition = _pointLight.transform.localPosition;

            var rotation = spotLightLocalRotation.eulerAngles.y - _offset;
            var x = Cos(-Deg2Rad * rotation) * _radius;
            var y = pointLightPosition.y;
            var z = Sin(-Deg2Rad * rotation) * _radius;
            _pointLight.transform.localPosition = new Vector3(x, y, z);

            // Slow Trigger
            direction = targetPosition - _slowTrigger.transform.position;
            targetRotation = Quaternion.LookRotation(direction);

            var slowTriggerTargetRotation = _slowTrigger.transform.rotation;
            slowTriggerTargetRotation = Quaternion.Slerp(slowTriggerTargetRotation, targetRotation, Time.deltaTime);
            _slowTrigger.transform.rotation = slowTriggerTargetRotation;
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