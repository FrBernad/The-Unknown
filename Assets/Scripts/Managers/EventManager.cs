using System;
using UnityEngine;

namespace Managers
{
    public class EventManager : MonoBehaviour
    {
        public static EventManager instance;


        #region UNITY_EVENTS

        private void Awake()
        {
            if (instance != null) Destroy(this);
            instance = this;
        }

        #endregion

        // #region GAME_OVER_EVENTS
        //
        // public event Action<bool> OnGameOver;
        //
        // public void EventGameOver(bool isVictory)
        // {
        //     if (OnGameOver != null) OnGameOver(isVictory);
        // }
        //
        // #endregion

        #region UI_EVENTS

        //Evento
        public event Action<int, int> OnInventoryChange;
        public event Action<float> OnStaminaChange;

        public event Action<bool> OnItemInventoryCollision;

        // Método de activación del evento
        public void InventoryChange(int currentItems, int maxItems)
        {
            //Si hay alguien suscripto
            if (OnInventoryChange != null) OnInventoryChange(currentItems, maxItems);
        }

        public void StaminaChange(float currentStamina)
        {
            if (OnStaminaChange != null) OnStaminaChange(currentStamina);
        }

        public void ItemInventoryCollision(bool show)
        {
            if (OnItemInventoryCollision != null) OnItemInventoryCollision(show);
        }

        #endregion
    }
}