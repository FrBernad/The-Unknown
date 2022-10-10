using System;
using System.Collections.Generic;
using Managers;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private int _maxItems;
    private int _currentItems = 0;

    private void Start()
    {
        UI_Inventory_Updater();
    }

    public void PickUpItem()
    {
        _currentItems++;
        //Llamo al método que dispara el evento
        UI_Inventory_Updater();
    }

    private void UI_Inventory_Updater()
    {
        EventManager.instance.InventoryChange(_currentItems, _maxItems);
    }
}