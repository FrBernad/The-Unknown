using System;
using Entities;
using UnityEngine;
using Color = System.Drawing.Color;

namespace DefaultNamespace
{
    public class NoteOutline : MonoBehaviour
    {
        private Outline _outline;
        private SphereCollider _collider;

        private void Start()
        {
            _outline = GetComponentInParent<Outline>();
            _outline.enabled = false;
            BoxCollider parentCollider = GetComponentInParent<BoxCollider>();
            _collider = GetComponent<SphereCollider>();
            Physics.IgnoreCollision(_collider, parentCollider);
        }

        private void OnTriggerEnter(Collider collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                _outline.enabled = true;
                _outline.OutlineColor = UnityEngine.Color.black;
            }
        }

        private void OnTriggerStay(Collider collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                Character character = collision.GetComponent<Character>();
                float normalizedDistance =
                    (character.transform.position - transform.position).magnitude / (_collider.radius / 2);
                float rgb = 1 - normalizedDistance;
                _outline.OutlineColor = new UnityEngine.Color(rgb, rgb, rgb);
            }
        }

        private void OnTriggerExit(Collider collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                _outline.enabled = false;
                _outline.OutlineColor = UnityEngine.Color.black;
            }
        }
    }
}