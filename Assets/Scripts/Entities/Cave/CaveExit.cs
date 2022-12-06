using Managers;
using UnityEngine;

namespace Entities.Cave
{
    public class CaveExit : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            EventManager.instance.ChangeAmbience(Ambience.Forest);
        }
    }
}