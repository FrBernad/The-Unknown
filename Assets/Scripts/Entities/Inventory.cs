using System;
using System.Collections.Generic;
using Managers;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int MaxItems => _maxItems;
    [SerializeField] private int _maxItems;
    public int CurrentItems => _currentItems;
    private int _currentItems = 0;

    private bool _inventoryIsFull = false;

    private void Start()
    {
        UI_Inventory_Updater();
    }

    public void StoreItem()
    {
        _currentItems++;
        //Llamo al método que dispara el evento
        UI_Inventory_Updater();
    }

    public bool InventoryIsFull()
    {
        return _currentItems == _maxItems;
    }

    private void UI_Inventory_Updater()
    {
        EventManager.instance.InventoryChange(_currentItems, _maxItems);
    }
}