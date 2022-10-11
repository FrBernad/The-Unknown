using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;

public class Note : MonoBehaviour
{
    public void Pickup()
    {
        Destroy(gameObject);
    }
}