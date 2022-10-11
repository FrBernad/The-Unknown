using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;

public class Item : MonoBehaviour
{
    public void PickUpItem()
    {
        Destroy(this.gameObject);
    }
}