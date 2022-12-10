using System;
using UnityEngine;

namespace Flyweight
{
    [CreateAssetMenu(fileName = "CharacterStats", menuName = "Stats/Character", order = 0)]
    public class CharacterStats : ScriptableObject
    {
        [SerializeField] private CharacterStatValues _characterStatValues;

        public float MaxStamina => _characterStatValues.MaxStamina;

        public float Speed => _characterStatValues.Speed;
        public float SlowedSpeed => _characterStatValues.SlowedSpeed;
        public float RotationSpeed => _characterStatValues.RotationSpeed;

        public float JumpHeight => _characterStatValues.JumpHeight;
    }

    [Serializable]
    public struct CharacterStatValues
    {
        public float MaxStamina;

        public float Speed;

        public float SlowedSpeed;

        public float RotationSpeed;

        public float JumpHeight;
    }
}