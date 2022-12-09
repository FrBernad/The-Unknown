using Managers;
using UnityEngine;

namespace Entities
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField] private int _maxItems = 10;
        public int GetMaxItems() => _maxItems;

        [SerializeField] private int _currentItems;

        private void Start()
        {
            NotifyInventoryUpdate();
        }

        public void StoreItem()
        {
            _currentItems++;
            //Llamo al método que dispara el evento
            NotifyInventoryUpdate();
        }

        public bool IsFull()
        {
            return _currentItems >= _maxItems;
        }

        private void NotifyInventoryUpdate()
        {
            EventManager.instance.InventoryChange(_currentItems, _maxItems);
        }
    }
}