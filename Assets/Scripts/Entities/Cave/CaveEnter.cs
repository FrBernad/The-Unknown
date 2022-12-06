using Managers;
using UnityEngine;

namespace Entities.Cave
{
    public class CaveEnter : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            EventManager.instance.ChangeAmbience(Ambience.Cave);
        }
    }
}