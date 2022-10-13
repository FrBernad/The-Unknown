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
            OnUIPanelUpdate(null);
        }

        private void Start()
        {
            // Realizo la suscripción al evento con el método que se va a suscribir
            EventManager.instance.OnInventoryChange += OnInventoryChange;
            EventManager.instance.OnStaminaChange += OnStaminaChange;
            EventManager.instance.OnUIPanelUpdate += OnUIPanelUpdate;
        }

        private void OnInventoryChange(int currentItems, int maxItems)
        {
            _inventoryItems.text = $"{currentItems} / {maxItems}";
        }

        private void OnStaminaChange(float currentStamina)
        {
            _staminaBar.fillAmount = currentStamina;
        }

        private void OnUIPanelUpdate(string message)
        {
            _messagePanel.SetActive(message != null);
            _panelText.text = message;
        }
    }
}