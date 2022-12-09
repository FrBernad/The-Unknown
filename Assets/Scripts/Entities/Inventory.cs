using Managers;
using UnityEngine;

namespace Entities
{
    public class Inventory : MonoBehaviour
    {
        public int MaxItems => 8;

        public int CurrentItems { get; private set; }

        private void Start()
        {
            NotifyInventoryUpdate();
        }

        public void StoreItem()
        {
            CurrentItems++;
            //Llamo al método que dispara el evento
            NotifyInventoryUpdate();
        }

        public bool IsFull()
        {
            return CurrentItems >= MaxItems;
        }

        private void NotifyInventoryUpdate()
        {
            EventManager.instance.InventoryChange(CurrentItems, MaxItems);
        }
    }
}