using System;
using System.Collections.Generic;
using Managers;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int MaxItems => _maxItems;
    [SerializeField] private int _maxItems = 8;
    public int CurrentItems => _currentItems;
    private int _currentItems = 0;

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

    public bool IsFull()
    {
        return _currentItems == _maxItems;
    }

    private void UI_Inventory_Updater()
    {
        EventManager.instance.InventoryChange(_currentItems, _maxItems);
    }
}