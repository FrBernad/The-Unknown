using System;
using Strategy;
using Entities;
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

        #region GAME_OVER_EVENTS

        public event Action<bool> OnGameOver;

        public void GameOver(bool isVictory)
        {
            if (OnGameOver != null) OnGameOver(isVictory);
        }

        #endregion

        #region UI_EVENTS

        //Evento
        public event Action<int, int> OnInventoryChange;
        public event Action<float> OnStaminaChange;
        public event Action<Ambience> OnChangeAmbience;
        public event Action<string> OnUIPanelUpdate;
        public event Action<ChargeStatus> OnChargeChange;

        public event Action<bool> OnStartConsumingBattery;

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

        public void UIPanelUpdate(string message)
        {
            if (OnUIPanelUpdate != null) OnUIPanelUpdate(message);
        }

        public void ChargeChange(ChargeStatus currentStatus)
        {
            if (OnChargeChange != null) OnChargeChange(currentStatus);
        }

        public void StartConsumingBattery(bool consumeBattery)
        {
            if (OnStartConsumingBattery != null) OnStartConsumingBattery(consumeBattery);
        }

        public void ChangeAmbience(Ambience ambience)
        {
            if (OnChangeAmbience != null) OnChangeAmbience(ambience);
        }

        #endregion
    }
}