using System;
using System.Collections;
using UnityEngine;

namespace Entities
{
    public class WaterPlane : MonoBehaviour
    {
        private GameObject _plane;

        private void Start()
        {
            _plane = transform.GetChild(0).gameObject;
            _plane.SetActive(false);
        }

        private void OnTriggerEnter(Collider collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                StartCoroutine(DoDelayed(() => _plane.SetActive(true)));
            }
        }

        private void OnTriggerExit(Collider collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                StartCoroutine(DoDelayed(() => _plane.SetActive(false)));
            }
        }

        private IEnumerator DoDelayed(Action action)
        {
            yield return null;
            if (action != null) action();
        }
    }
}