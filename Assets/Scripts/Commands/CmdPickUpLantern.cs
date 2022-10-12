using Interfaces;
using Managers;
using UnityEngine;

namespace Commands
{
    public class CmdPickUpLantern : ICommand
    {
        private Flashlight _floorFlashlight;
        private Flashlight _flashlight;
        private AudioSource _audioSource;
        private AudioClip _pickupFlashlightClip;

        public CmdPickUpLantern(Flashlight floorFlashlight, Flashlight flashlight, AudioSource audioSource,
            AudioClip pickupFlashlightClip)
        {
            _floorFlashlight = floorFlashlight;
            _flashlight = flashlight;
            _audioSource = audioSource;
            _pickupFlashlightClip = pickupFlashlightClip;
        }

        public void Execute()
        {
            _floorFlashlight.Pickup();
            _flashlight.gameObject.SetActive(true);
            _audioSource.PlayOneShot(_pickupFlashlightClip);
        }
    }
}