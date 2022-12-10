using UnityEngine;

namespace Entities
{
    public class WaterPlane : MonoBehaviour
    {
        private GameObject _plane;
        private Renderer _planeRenderer;

        private void Start()
        {
            _plane = transform.GetChild(0).gameObject;
            _planeRenderer = _plane.GetComponent<Renderer>();
            _planeRenderer.enabled = false;
        }

        private void OnTriggerEnter(Collider collision)
        {
            if (collision.gameObject.CompareTag("Player"))
                StartCoroutine(Utils.Utils.DoDelayed(0f, () => _planeRenderer.enabled = true));
        }

        private void OnTriggerExit(Collider collision)
        {
            if (collision.gameObject.CompareTag("Player"))
                StartCoroutine(Utils.Utils.DoDelayed(0, () => _planeRenderer.enabled = false));
        }
    }
}