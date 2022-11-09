using Interfaces;
using Managers;
using UnityEngine;

public class Flashlight : MonoBehaviour, IPickable
{
    private AudioSource _audioSource;
    private GameObject _light;
    private bool isOn = true;

    private void Start()
    {
        _light = transform.GetChild(0).gameObject;
        _light.SetActive(isOn);

        _audioSource = gameObject.GetComponent<AudioSource>();
    }

    public void Pickup()
    {
        UpdateUIPanel(null);
        Destroy(gameObject);
    }

    public void Switch()
    {
        isOn = !isOn;
        _light.SetActive(isOn);
        _audioSource.Play();
    }

    private void UpdateUIPanel(string message)
    {
        EventManager.instance.UIPanelUpdate(message);
    }
}