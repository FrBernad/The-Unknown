using System;
using System.Collections.Generic;
using Factory;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Entities.Monster
{
    public class MonsterSpawner : MonoBehaviour
    {
        private readonly MonsterFactory _monsterFactory = new MonsterFactory();
        [SerializeField] private Monster _monster;
        [SerializeField] private List<Transform> _entitiesSpawnPoints;
        [SerializeField] private AudioSource _globalAudioSource;
        [SerializeField] private AudioClip _monsterSpawnAudioClip;

        private void Start()
        {
            SetSpawnPoints();
        }

        private void SetSpawnPoints()
        {
            var spawnPoints = GameObject.Find("MonsterSpawnPoints");
            var totalSpawnPoints = spawnPoints.transform.childCount;

            _entitiesSpawnPoints = new List<Transform>(totalSpawnPoints);

            for (var i = 0; i < totalSpawnPoints; i++) _entitiesSpawnPoints.Add(spawnPoints.transform.GetChild(i));
        }

        private void SetSpawnPosition(Component obj)
        {
            var spawnPoint = Random.Range(0, _entitiesSpawnPoints.Count);
            obj.transform.position = _entitiesSpawnPoints[spawnPoint].position;
        }

        public Monster SpawnMonster()
        {
            var monster = _monsterFactory.Create(_monster);
            SetSpawnPosition(_monster);
            _globalAudioSource.PlayOneShot(_monsterSpawnAudioClip);
            return monster;
        }
    }
}