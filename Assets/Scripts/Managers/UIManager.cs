using System.Collections.Generic;
using Strategy;
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
        [SerializeField] private GameObject _flashlightBatteryGameObject;
        [SerializeField] private Image _flashlightBatteryImage;

        private ChargeStatus _currentChargeStatus;
        [SerializeField] private Sprite _emptyCharge;
        [SerializeField] private Sprite _quarterCharge;
        [SerializeField] private Sprite _halfCharge;
        [SerializeField] private Sprite _threeQuarterCharge;
        [SerializeField] private Sprite _fullCharge;


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
            EventManager.instance.OnChargeChange += OnChargeChange;
            _flashlightBatteryGameObject.SetActive(false);
            _flashlightBatteryImage = _flashlightBatteryGameObject.GetComponentInChildren<Image>(true);
        }

        private void OnInventoryChange(int currentItems, int maxItems)
        {
            _inventoryItems.text = $"{currentItems} / {maxItems}";
        }

        private void OnStaminaChange(float currentStamina)
        {
            _staminaBar.fillAmount = currentStamina;
        }

        private void OnChargeChange(ChargeStatus chargeStatus)
        {
            if (!_flashlightBatteryImage.IsActive())
                _flashlightBatteryImage.gameObject.SetActive(true);

            if (chargeStatus != _currentChargeStatus)
            {
                _flashlightBatteryImage.sprite = GetChargeStatusSprite(chargeStatus);
            }
        }

        private Sprite GetChargeStatusSprite(ChargeStatus status)
        {
            Sprite sprite;
            switch (status)
            {
                case ChargeStatus.Empty:
                    sprite = _emptyCharge;
                    break;
                case ChargeStatus.QuarterCharge:
                    sprite = _quarterCharge;
                    break;
                case ChargeStatus.HalfCharge:
                    sprite = _halfCharge;
                    break;
                case ChargeStatus.ThreeQuartersCharge:
                    sprite = _threeQuarterCharge;
                    break;
                default:
                    sprite = _fullCharge;
                    break;
            }

            return sprite;
        }

        private void OnUIPanelUpdate(string message)
        {
            _messagePanel.SetActive(message != null);
            _panelText.text = message;
        }
    }
}