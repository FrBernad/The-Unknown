using UnityEngine;

namespace Interfaces
{
    public interface IMovable
    {
        float Speed { get; }
        float RotationSpeed { get; }
        bool Sprinting { get; }

        void Move(Vector3 direction);

        void Jump(Vector3 direction);

        void Rotate(Vector3 direction);
    }
}