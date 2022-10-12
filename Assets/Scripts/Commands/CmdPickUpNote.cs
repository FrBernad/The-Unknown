using System;
using Interfaces;
using Managers;
using UnityEngine;

namespace Commands
{
    public class CmdPickUpNote : ICommand
    {
        private Note _note;
        private Inventory _inventory;
        private AudioSource _audioSource;
        private AudioClip _pickupAudioClip;

        public CmdPickUpNote(Note note, Inventory inventory, AudioSource audioSource, AudioClip pickupAudioClip)
        {
            _note = note;
            _inventory = inventory;
            _audioSource = audioSource;
            _pickupAudioClip = pickupAudioClip;
        }

        public void Execute()
        {
            _note.Pickup();
            _inventory.StoreItem();
            _audioSource.PlayOneShot(_pickupAudioClip);
        }
    }
}