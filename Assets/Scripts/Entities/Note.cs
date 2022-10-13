using Interfaces;
using UnityEngine;

public class Note : MonoBehaviour, IPickable
{
    public void Pickup()
    {
        Destroy(gameObject);
    }
}