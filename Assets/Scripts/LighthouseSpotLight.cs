using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LighthouseSpotLight : MonoBehaviour
{
    public float Speed => _speed;
    [SerializeField] private float _speed = 0.1F;

    void Update()
    {
        transform.Rotate(Vector3.up * Mathf.Rad2Deg * (Time.deltaTime * Speed));
    }
}