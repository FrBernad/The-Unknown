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
                StartCoroutine(Utils.Utils.DoDelayed(0f, () => _plane.SetActive(true)));
        }

        private void OnTriggerExit(Collider collision)
        {
            if (collision.gameObject.CompareTag("Player"))
                StartCoroutine(Utils.Utils.DoDelayed(0, () => _plane.SetActive(false)));
        }
    }
}