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
        private float _maxCharge;
        [SerializeField] private bool _isChargeable;
        [SerializeField] private float _decreaseChargeValue = 0.001f;
        [SerializeField] private float _increaseChargeValue = 25f;


        private void Start()
        {
            _light = transform.GetChild(0).gameObject;
            _isChargeable = false;
            _light.SetActive(_isOn);
            _audioSource = gameObject.GetComponent<AudioSource>();
            _maxCharge = 100;
            _currentCharge = _maxCharge;
        }


        public void Pickup()
        {
            UpdateUIPanel(null);
            Destroy(gameObject);
            UpdateChargeUI();
        }

        public void Switch()
        {
            if (HasCharge())
            {
                _isOn = !_isOn;
                _light.SetActive(_isOn);
            }

            if (_isOn) StartConsumingBattery();
            else StopConsumingBattery();

            _audioSource.Play();
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
                _currentCharge = 0;
                StopConsumingBattery();
            }

            UpdateChargeUI();
        }

        public void IncreaseCharge()
        {
            _currentCharge += _increaseChargeValue;

            if (_currentCharge > _maxCharge) _currentCharge = _maxCharge;

            UpdateChargeUI();
        }


        public void UpdateChargeUI()
        {
            ChargeStatus status;
            if (_currentCharge <= 100 && _currentCharge > 75) status = ChargeStatus.FullCharge;
            else if (_currentCharge <= 75 && _currentCharge > 50) status = ChargeStatus.ThreeQuartersCharge;
            else if (_currentCharge <= 50 && _currentCharge > 25) status = ChargeStatus.HalfCharge;
            else if (_currentCharge <= 25 && _currentCharge > 0) status = ChargeStatus.QuarterCharge;
            else status = ChargeStatus.Empty;

            EventManager.instance.ChargeChange(status);
        }

        private bool HasCharge()
        {
            return _currentCharge > 0;
        }

        public void setIsChargeable(bool isChargeable)
        {
            _isChargeable = isChargeable;
            if (_isOn)
            {
                StartConsumingBattery();
            }
        }

        public void StartConsumingBattery()
        {
            InvokeRepeating("DecreaseCharge", 0, 1);
        }

        public void StopConsumingBattery()
        {
            CancelInvoke("DecreaseCharge");
        }
    }
}