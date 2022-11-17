using Entities;
using Entities.Note;
using Strategy;
using UnityEngine;

namespace Commands
{
    public class CmdPickUpNote : ICommand
    {
        private readonly AudioSource _audioSource;
        private readonly Inventory _inventory;
        private readonly Note _note;
        private readonly AudioClip _pickupAudioClip;

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