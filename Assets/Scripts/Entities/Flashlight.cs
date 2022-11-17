using Managers;
using Strategy;
using UnityEngine;

namespace Entities
{
    public class Flashlight : MonoBehaviour, IPickable
    {
        private AudioSource _audioSource;
        private GameObject _light;
        private bool _isOn = true;

        private void Start()
        {
            _light = transform.GetChild(0).gameObject;
            _light.SetActive(_isOn);

            _audioSource = gameObject.GetComponent<AudioSource>();
        }

        public void Pickup()
        {
            UpdateUIPanel(null);
            Destroy(gameObject);
        }

        public void Switch()
        {
            _isOn = !_isOn;
            _light.SetActive(_isOn);
            _audioSource.Play();
        }

        private void UpdateUIPanel(string message)
        {
            EventManager.instance.UIPanelUpdate(message);
        }
    }
}