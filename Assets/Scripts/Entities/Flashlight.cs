using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    private Light _light;
    private bool isOn = true;

    private AudioSource _audioSource;

    void Start()
    {
        _light = gameObject.GetComponentInChildren<Light>(true);
        _light.gameObject.SetActive(isOn);

        _audioSource = gameObject.GetComponent<AudioSource>();
    }

    public void Switch()
    {
        isOn = !isOn;
        _light.gameObject.SetActive(isOn);
        _audioSource.Play();
    }

    public void Pickup()
    {
        Destroy(gameObject);
    }
}