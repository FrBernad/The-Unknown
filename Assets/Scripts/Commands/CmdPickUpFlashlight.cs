using Interfaces;
using UnityEngine;

namespace Commands
{
    public class CmdPickUpFlashlight : ICommand
    {
        private readonly AudioSource _audioSource;
        private readonly Flashlight _flashlight;
        private readonly Flashlight _floorFlashlight;
        private readonly AudioClip _pickupFlashlightClip;

        public CmdPickUpFlashlight(Flashlight floorFlashlight, Flashlight flashlight, AudioSource audioSource,
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