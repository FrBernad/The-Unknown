using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterController : MonoBehaviour
{
    private NavMeshAgent _agent;
    private Transform _objective;
    private AudioSource _nearbyAudioSource;
    [SerializeField] private AudioClip _nearbyAudioClip;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _objective = GameObject.FindGameObjectWithTag("Player").transform;
        _nearbyAudioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (_objective != null)
        {
            _agent.SetDestination(_objective.position);
            UpdateNearbySound();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
        }
    }

    private void UpdateNearbySound()
    {
        bool isNearby = (_objective.position - transform.position).magnitude <= _nearbyAudioSource.maxDistance;

        _nearbyAudioSource.enabled = isNearby;

        if (isNearby && !_nearbyAudioSource.isPlaying)
        {
            _nearbyAudioSource.Play();
        }

        if (!isNearby && _nearbyAudioSource.isPlaying)
        {
            _nearbyAudioSource.Stop();
        }
    }
}