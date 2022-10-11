using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;

public class Item : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        UpdateUIItemPanel(false);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            UpdateUIItemPanel(true);
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            UpdateUIItemPanel(false);
        }
    }

    public static void UpdateUIItemPanel(bool show)
    {
        EventManager.instance.ItemInventoryCollision(show);
    }
}