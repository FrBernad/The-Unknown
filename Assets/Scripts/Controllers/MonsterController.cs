using System;
using System.Collections.Generic;
using Managers;
using UnityEngine;
using UnityEngine.AI;

namespace Controllers
{
    public class MonsterController : MonoBehaviour
    {
        private NavMeshAgent _agent;
        private Transform _objective;
        private AudioSource _nearbyAudioSource;

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

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                EventManager.instance.GameOver(false);
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
}