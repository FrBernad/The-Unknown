using Entities;
using UnityEngine;

namespace Controllers
{
    public class SplineTrajectoryController : MonoBehaviour
    {
        [SerializeField] private Spline _spline;
        [SerializeField] private Transform _targetPosition;

        private void Update()
        {
            (Vector3 pos, Quaternion rot) result = _spline.PositionOnSpline(_targetPosition.position);
            var tr = transform;
            tr.position = result.pos;
            tr.rotation = result.rot;
        }
    }
}