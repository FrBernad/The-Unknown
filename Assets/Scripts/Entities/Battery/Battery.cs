using Managers;
using Strategy;
using UnityEngine;

namespace Entities.Battery
{
    public class Battery : MonoBehaviour, IPickable
    {
        public void Pickup()
        {
            UpdateUIPanel(null);
            gameObject.SetActive(false);
        }

        private void UpdateUIPanel(string message)
        {
            EventManager.instance.UIPanelUpdate(message);
        }
    }
}