using System;
using Managers;
using Strategy;
using UnityEngine;

namespace Entities
{
    public class Flashlight : MonoBehaviour, IPickable, IRechargeable
    {
        private AudioSource _audioSource;
        private GameObject _light;
        private bool _isOn = true;
        [SerializeField] private float _currentCharge;
        private float _maxCharge = 100;
        [SerializeField] private bool _isChargeable;
        [SerializeField] private float _decreaseChargeValue = 5;
        [SerializeField] private float _increaseChargeValue = 25;
        [SerializeField] private int _decreaseChargeDelay = 9;


        private void Start()
        {
            _light = transform.GetChild(0).gameObject;
            _light.SetActive(_isOn);
            _audioSource = gameObject.GetComponent<AudioSource>();
            _currentCharge = _maxCharge;
            //significa que ocurrio el evento para que se descargue antes que la agarre
            if (_isChargeable)
            {
                StartConsumingBattery();
            }
        }


        public void Pickup()
        {
            UpdateUIPanel(null);
            Destroy(gameObject);
            ChargeStatus currentChargeStatus = GetChargeStatus();
            UpdateChargeUI(currentChargeStatus);
        }

        public void Switch()
        {
            if (HasCharge())
            {
                SwitchLight();
            }

            if (_isChargeable)
            {
                if (_isOn) StartConsumingBattery();
                else StopConsumingBattery();
            }


            _audioSource.Play();
        }

        private void SwitchLight()
        {
            _isOn = !_isOn;
            _light.SetActive(_isOn);
        }

        private void UpdateUIPanel(string message)
        {
            EventManager.instance.UIPanelUpdate(message);
        }


        public void DecreaseCharge()
        {
            _currentCharge -= _decreaseChargeValue;
            if (_currentCharge < 0)
            {
                SwitchLight();
                _currentCharge = 0;
                StopConsumingBattery();
            }

            ChargeStatus currentChargeStatus = GetChargeStatus();
            UpdateChargeUI(currentChargeStatus);
        }

        public void IncreaseCharge()
        {
            _currentCharge += _increaseChargeValue;

            if (_currentCharge > _maxCharge) _currentCharge = _maxCharge;

            ChargeStatus currentChargeStatus = GetChargeStatus();

            UpdateChargeUI(currentChargeStatus);
        }


        public void UpdateChargeUI(ChargeStatus currentChargeStatus)
        {
            EventManager.instance.ChargeChange(currentChargeStatus);
        }

        private ChargeStatus GetChargeStatus()
        {
            ChargeStatus status;
            if (_currentCharge <= 100 && _currentCharge > 75) status = ChargeStatus.FullCharge;
            else if (_currentCharge <= 75 && _currentCharge > 50) status = ChargeStatus.ThreeQuartersCharge;
            else if (_currentCharge <= 50 && _currentCharge > 25) status = ChargeStatus.HalfCharge;
            else if (_currentCharge <= 25 && _currentCharge > 0) status = ChargeStatus.QuarterCharge;
            else status = ChargeStatus.Empty;

            return status;
        }

        private bool HasCharge()
        {
            return _currentCharge > 0;
        }

        public void SetIsChargeable(bool isChargeable)
        {
            _isChargeable = isChargeable;
            //active.self es true cuando la linterna fue agarrada y además verifico que esté encendida.
            if (gameObject.activeSelf && _isOn)
            {
                StartConsumingBattery();
            }
        }

        public void StartConsumingBattery()
        {
            InvokeRepeating("DecreaseCharge", 0, _decreaseChargeDelay);
        }

        public void StopConsumingBattery()
        {
            CancelInvoke("DecreaseCharge");
        }
    }
}