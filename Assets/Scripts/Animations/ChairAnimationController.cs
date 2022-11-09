using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChairAnimationController : MonoBehaviour
{
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SetAnimatorParameters(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SetAnimatorParameters(false);
        }
    }

    private void SetAnimatorParameters(bool performAnimation)
    {
        _animator.SetBool("IsNear", performAnimation);
    }
}