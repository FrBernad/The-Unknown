using System;
using Managers;
using UnityEngine;

namespace Entities.Lighthouse
{
    public class SlowTrigger : MonoBehaviour
    {
        public bool isTriggered;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                isTriggered = true;
                EventManager.instance.SlowPlayer(true);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                isTriggered = false;
                EventManager.instance.SlowPlayer(false);
            }
        }
    }
}