using System;
using Entities;
using Managers;
using UnityEngine;

namespace Controllers
{
    public class LifeController : MonoBehaviour
    {
        public float MaxStamina => GetComponent<Character>().CharacterStats.MaxStamina;
        [SerializeField] private float _currentStamina;

        public float CurrentStamina => _currentStamina;

        public void Start()
        {
            _currentStamina = MaxStamina;
            UpdateStaminaUI();
        }

        public void decreaseStamina(float stamina)
        {
            _currentStamina -= stamina;
            if (_currentStamina < 0)
            {
                _currentStamina = 0;
            }

            UpdateStaminaUI();
        }

        public void increaseStamina(float stamina)
        {
            _currentStamina += stamina;

            if (_currentStamina > MaxStamina)
            {
                _currentStamina = MaxStamina;
            }

            UpdateStaminaUI();
        }

        public void UpdateStaminaUI()
        {
            EventManager.instance.StaminaChange(_currentStamina / 100);
        }
    }
}