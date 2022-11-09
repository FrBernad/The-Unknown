using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChairAnimationController : MonoBehaviour
{
    private Animator _animator;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip audioClip;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = audioClip;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SetAnimatorParameters(true);
            audioSource.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SetAnimatorParameters(false);
            audioSource.Stop();
        }
    }

    private void SetAnimatorParameters(bool performAnimation)
    {
        _animator.SetBool("IsNear", performAnimation);
    }
}