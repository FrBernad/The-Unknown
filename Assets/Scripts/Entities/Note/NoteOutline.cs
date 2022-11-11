using Entities;
using UnityEngine;

namespace Entities.Note
{
    public class NoteOutline : MonoBehaviour
    {
        private SphereCollider _collider;
        private Outline _outline;

        private void Start()
        {
            _outline = GetComponentInParent<Outline>();
            _outline.enabled = false;
            var parentCollider = GetComponentInParent<BoxCollider>();
            _collider = GetComponent<SphereCollider>();
            Physics.IgnoreCollision(_collider, parentCollider);
        }

        private void OnTriggerEnter(Collider collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                _outline.enabled = true;
                _outline.OutlineColor = Color.black;
            }
        }

        private void OnTriggerExit(Collider collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                _outline.enabled = false;
                _outline.OutlineColor = Color.black;
            }
        }

        private void OnTriggerStay(Collider collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                var character = collision.GetComponent<Character>();
                var normalizedDistance =
                    (character.transform.position - transform.position).magnitude / (_collider.radius / 2);
                var rgb = 1 - normalizedDistance;
                _outline.OutlineColor = new Color(rgb, rgb, rgb);
            }
        }
    }
}