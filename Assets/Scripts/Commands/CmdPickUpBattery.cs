using Entities;
using Entities.Battery;
using Strategy;
using UnityEngine;

namespace Commands
{
    public class CmdPickUpBattery : ICommand
    {
        private readonly AudioSource _audioSource;
        private readonly Battery _battery;
        private readonly AudioClip _pickupBatteryClip;

        public CmdPickUpBattery(Battery battery, AudioSource audioSource, AudioClip pickupFlashlightClip)
        {
            _battery = battery;
            _audioSource = audioSource;
            _pickupBatteryClip = pickupFlashlightClip;
        }

        public void Execute()
        {
            _battery.Pickup();
            _audioSource.PlayOneShot(_pickupBatteryClip);
        }
    }
}