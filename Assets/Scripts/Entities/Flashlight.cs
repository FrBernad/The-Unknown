using UnityEngine;

public class Flashlight : MonoBehaviour
{
    [SerializeField] private GameObject _light;
    private bool isOn = true;

    [SerializeField] private AudioSource _audioSource;

    void Start()
    {
        _light = GameObject.Find("LightOuter");
        _light.SetActive(isOn);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            isOn = !isOn;
            if (isOn)
            {
                _light.SetActive(true);
                _audioSource.Play();
            }
            else
            {
                _light.SetActive(false);
                _audioSource.Play();
            }
        }
    }
}