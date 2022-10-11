using System;
using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
    public class UIManager : MonoBehaviour
    {
        // Guardo las referencias a los objetos de Unity
        [SerializeField] private Text _inventoryItems;
        [SerializeField] private Image _staminaBar;
        [SerializeField] private GameObject _messagePanel;
        [SerializeField] private Text _panelText;

        private void Awake()
        {
            ShowActionPanel(false, "");
        }

        private void Start()
        {
            // Realizo la suscripción al evento con el método que se va a suscribir
            EventManager.instance.OnInventoryChange += UpdateInventoryChange;
            EventManager.instance.OnStaminaChange += UpdateStaminaChange;
            EventManager.instance.OnUIPanelUpdate += ShowActionPanel;
        }

        private void UpdateInventoryChange(int currentItems, int maxItems)
        {
            _inventoryItems.text = $"{currentItems} / {maxItems}";
        }

        private void UpdateStaminaChange(float currentStamina)
        {
            _staminaBar.fillAmount = currentStamina;
        }

        private void ShowActionPanel(bool show, string message)
        {
            _messagePanel.SetActive(show);
            _panelText.text = message;
        }
    }
}