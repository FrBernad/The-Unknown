using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LighthousePointLight : MonoBehaviour
{
    public float Speed => _speed;
    [SerializeField] private float _speed = 0.1F;

    public float Radius => _radius;
    [SerializeField] private float _radius = 3;

    void Update()
    {
        var x = (float)Math.Cos(-Time.time * _speed) * _radius;
        var y = transform.localPosition.y;
        var z = (float)Math.Sin(-Time.time * _speed) * _radius;

        transform.localPosition = new Vector3(x, y, z);
    }
}