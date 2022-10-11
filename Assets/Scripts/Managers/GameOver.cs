using System;
using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class GameOver : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private Text _text;
        [SerializeField] private bool _isVictory;
        [SerializeField] private AudioClip _victoryClip;
        [SerializeField] private AudioClip _defeatClip;
        private Color _backgroundColorVictory = new Color32(164, 227, 201, 255);
        private Color _backgroundColorDefeat = new Color32(212, 25, 25, 255);
        [SerializeField] private string _message;

        private void Awake()
        {
            _isVictory = GlobalData.instance.IsVictory;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            _message = _isVictory ? "You survive" : "You die";
            AudioSource audioSource = GetComponent<AudioSource>();
            audioSource.clip = _isVictory ? _victoryClip : _defeatClip;
            audioSource.loop = false;
            audioSource.playOnAwake = true;
            audioSource.Play();
        }

        private void Start()
        {
            _text.text = _message;
            _image.color = _isVictory ? _backgroundColorVictory : _backgroundColorDefeat;
        }
    }
}