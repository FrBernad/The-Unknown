using Managers;
using Strategy;
using UnityEngine;

namespace Entities
{
    public class Battery : MonoBehaviour, IPickable
    {
        public void Pickup()
        {
            UpdateUIPanel(null);
            Destroy(gameObject);
        }

        private void UpdateUIPanel(string message)
        {
            EventManager.instance.UIPanelUpdate(message);
        }
    }
}