using System;
using Managers;
using UnityEngine;

namespace Controllers
{
    public class LifeController : MonoBehaviour
    {
        [SerializeField] private float _maxStamina = 100;
        [SerializeField] private float _currentStamina;

        public float MaxStamina => _maxStamina;
        public float CurrentStamina => _currentStamina;

        public void Start()
        {
            _currentStamina = _maxStamina;
            UI_Stamina_Updater();
        }

        public void decreaseStamina(float stamina)
        {
            _currentStamina -= stamina;
            if (_currentStamina < 0)
            {
                _currentStamina = 0;
            }

            UI_Stamina_Updater();
        }

        public void increaseStamina(float stamina)
        {
            _currentStamina += stamina;

            if (_currentStamina > _maxStamina)
            {
                _currentStamina = _maxStamina;
            }

            UI_Stamina_Updater();
        }

        public void UI_Stamina_Updater()
        {
            EventManager.instance.StaminaChange(_currentStamina / 100);
        }
    }
}