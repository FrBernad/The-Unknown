using System;
using UnityEngine;

public class Spline : MonoBehaviour
{
    public Vector3[] SplinePoints => _splinePoints;
    [SerializeField] private Vector3[] _splinePoints;

    public int SplineCount => _splineCount;
    [SerializeField] private int _splineCount;

    [SerializeField] private bool debugDrawSpline = true;

    private void Start()
    {
        _splineCount = transform.childCount;
        _splinePoints = new Vector3[_splineCount];

        for (int i = 0; i < _splineCount; i++)
        {
            _splinePoints[i] = transform.GetChild(i).position;
        }
    }

    private void Update()
    {
        if (_splineCount > 1 && debugDrawSpline)
        {
            for (int i = 0; i < _splineCount; i++)
            {
                Debug.DrawLine(_splinePoints[i], _splinePoints[(i + 1) % _splineCount], Color.green);
            }
        }
    }

    public Vector3 PositionOnSpline(Vector3 position)
    {
        int closest = GetClosestSplinePoint(position);

        int prevPoint = ((closest - 1) % _splineCount + _splineCount) % _splineCount;
        int nextPoint = (closest + 1) % _splineCount;
        return SplineProjection(_splinePoints[prevPoint], _splinePoints[closest], _splinePoints[nextPoint], position);
    }

    private int GetClosestSplinePoint(Vector3 position)
    {
        int closestPoint = -1;
        float shortestDistance = Single.PositiveInfinity;

        for (int i = 0; i < _splineCount; i++)
        {
            float distance = (_splinePoints[i] - position).sqrMagnitude;
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                closestPoint = i;
            }
        }

        return closestPoint;
    }

    private Vector3 SplineProjection(Vector3 prev, Vector3 current, Vector3 next, Vector3 position)
    {
        Vector3 currentToPos = position - current;
        Vector3 currentToNextNormalized = (next - current).normalized;
        Vector3 currentToPrevNormalized = (prev - current).normalized;

        float projectionCurrentNext = Vector3.Dot(currentToNextNormalized, currentToPos);

        if (projectionCurrentNext > 0.0f)
        {
            return projectionCurrentNext * currentToNextNormalized + current;
        }

        float projectionCurrentPrev = Vector3.Dot(currentToPrevNormalized, currentToPos);

        return projectionCurrentPrev * currentToPrevNormalized + current;
    }
}