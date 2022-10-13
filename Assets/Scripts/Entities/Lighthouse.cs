using System;
using UnityEngine;

namespace Entities
{
    public class Lighthouse : MonoBehaviour
    {
        [SerializeField] private GameObject _pointLight;
        [SerializeField] private GameObject _spotLight;

        [SerializeField] private float _speed = 0.1F;
        [SerializeField] private float _radius = 3;

        private void Update()
        {
            var x = (float)Math.Cos(-Time.time * _speed) * _radius;
            var y = _pointLight.transform.localPosition.y;
            var z = (float)Math.Sin(-Time.time * _speed) * _radius;

            _pointLight.transform.localPosition = new Vector3(x, y, z);

            _spotLight.transform.Rotate(Vector3.up * Mathf.Rad2Deg * (Time.deltaTime * _speed));
        }
    }
}