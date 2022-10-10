using System;
using UnityEngine;

public class SplineTrayectoryController : MonoBehaviour
{
    [SerializeField] private Spline _spline;
    [SerializeField] private Transform _targetPosition;

    private void Update()
    {
        transform.position = _spline.PositionOnSpline(_targetPosition.position);
    }
}