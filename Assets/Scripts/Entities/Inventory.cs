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
            UI_Inventory_Updater();
        }

        public void StoreItem()
        {
            CurrentItems++;
            //Llamo al método que dispara el evento
            UI_Inventory_Updater();
        }

        public bool IsFull()
        {
            return CurrentItems >= MaxItems;
        }

        private void UI_Inventory_Updater()
        {
            EventManager.instance.InventoryChange(CurrentItems, MaxItems);
        }
    }
}