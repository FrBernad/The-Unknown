using Commands;
using EventQueue;
using UnityEngine;
using UnityEngine.AI;

namespace Entities.Monster
{
    public class Monster : MonoBehaviour
    {
        private NavMeshAgent _agent;
        private CmdLose _cmdLose;
        private AudioSource _nearbyAudioSource;
        private Transform _objective;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            _objective = GameObject.FindGameObjectWithTag("Player").transform;
            _nearbyAudioSource = GetComponent<AudioSource>();
        }

        private void Start()
        {
            _cmdLose = new CmdLose();
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
            if (other.gameObject.CompareTag("Player")) EventQueueManager.instance.AddCommand(_cmdLose);
        }

        private void UpdateNearbySound()
        {
            var isNearby = (_objective.position - transform.position).magnitude <= _nearbyAudioSource.maxDistance;

            _nearbyAudioSource.enabled = isNearby;

            if (isNearby && !_nearbyAudioSource.isPlaying) _nearbyAudioSource.Play();

            if (!isNearby && _nearbyAudioSource.isPlaying) _nearbyAudioSource.Stop();
        }
    }
}