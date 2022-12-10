using System;
using System.Collections.Generic;
using System.Linq;
using Entities;
using Entities.Battery;
using Entities.Cave;
using Factory;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Entities.Ambience;
using Random = UnityEngine.Random;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private Monster _monster;
        [SerializeField] private List<Transform> _entitiesSpawnPoints;
        [SerializeField] private AudioSource _globalAudioSource;
        [SerializeField] private AudioClip _monsterSpawnAudioClip;
        [SerializeField] private AudioClip _caveOpenAudioClip;

        [SerializeField] private AudioSource _ambienceAudioSource;
        [SerializeField] private AudioClip _forestAmbienceAudioClip;
        [SerializeField] private AudioClip _caveAmbienceAudioClip;

        [SerializeField] private int _monsterRespawnTime = 10;
        [SerializeField] private int _monsterLifeTime = 30;

        [SerializeField] private AudioClip _screamerAudioClip;
        [SerializeField] private GameObject _screamerObject;
        [SerializeField] private GameObject _Hud;

        [SerializeField] private Lighthouse _lighthouse;
        [SerializeField] private int _notesToChangeLighthouseMode = 7;

        [SerializeField] private Cave _cave;
        [SerializeField] private int _notesToOpenCave = 8;

        [SerializeField] private BatteriesSpawner _batteriesSpawner;
        [SerializeField] private int _notesToStartDischarge = 1;
        [SerializeField] private int _batteriesRespawnTime = 20;
        [SerializeField] private int _batteriesLifeTime = 30;

        private MonsterFactory _monsterFactory = new MonsterFactory();

        private void Start()
        {
            SetSpawnPoints();
            StartCoroutine(MonsterLifecycle());
            EventManager.instance.OnGameOver += OnGameOver;
            EventManager.instance.OnInventoryChange += OnInventoryChange;
            EventManager.instance.OnChangeAmbience += OnChangeAmbience;
            StartCoroutine(DisplayInitialMessage());
        }

        private IEnumerator<WaitForSeconds> DisplayInitialMessage()
        {
            EventManager.instance.UIPanelUpdate("Grab all the notes and \n return to the boat");
            yield return new WaitForSeconds(4);
            EventManager.instance.UIPanelUpdate(null);
        }

        private IEnumerator<WaitForSeconds> MonsterLifecycle()
        {
            while (true)
            {
                yield return new WaitForSeconds(_monsterRespawnTime);

                var monster = SpawnMonster();

                yield return new WaitForSeconds(_monsterLifeTime);

                Destroy(monster.gameObject);
            }
        }

        private void SetSpawnPoints()
        {
            var spawnPoints = GameObject.Find("EntitiesSpawnPoints");
            var totalSpawnPoints = spawnPoints.transform.childCount;

            _entitiesSpawnPoints = new List<Transform>(totalSpawnPoints);

            for (var i = 0; i < totalSpawnPoints; i++) _entitiesSpawnPoints.Add(spawnPoints.transform.GetChild(i));
        }

        private void SetSpawnPosition(Component obj)
        {
            var spawnPoint = Random.Range(0, _entitiesSpawnPoints.Count);
            obj.transform.position = _entitiesSpawnPoints[spawnPoint].position;
        }

        private Monster SpawnMonster()
        {
            var monster = _monsterFactory.Create(_monster);
            SetSpawnPosition(_monster);
            _globalAudioSource.PlayOneShot(_monsterSpawnAudioClip);
            return monster;
        }


        private void OnGameOver(bool isVictory)
        {
            GlobalData.instance.SetVictoryField(isVictory);
            if (!isVictory)
            {
                _globalAudioSource.PlayOneShot(_screamerAudioClip);
                _Hud.SetActive(false);
                _screamerObject.SetActive(true);
                StartCoroutine(Utils.Utils.DoDelayed(2.5f, () => SceneManager.LoadScene("Game Over")));
            }
            else
            {
                SceneManager.LoadScene("Game Over");
            }
        }


        private void OnInventoryChange(int currentItems, int maxItems)
        {
            CheckCaveState(currentItems);
            CheckBatteriesState(currentItems);
            CheckLighthouseState(currentItems);
        }


        private void CheckCaveState(int currentItems)
        {
            if (currentItems == _notesToOpenCave && !_cave.isOpen)
            {
                _globalAudioSource.PlayOneShot(_caveOpenAudioClip);
                _cave.Open();
            }
        }

        private void CheckBatteriesState(int currentItems)
        {
            if (currentItems == _notesToStartDischarge)
            {
                Flashlight flashlight = GameObject.FindWithTag("Player").GetComponentInChildren<Flashlight>(true);
                flashlight.SetIsChargeable(true);
                StartCoroutine(BatteriesLifecycle());
            }
        }

        private IEnumerator<WaitForSeconds> BatteriesLifecycle()
        {
            while (true)
            {
                yield return new WaitForSeconds(_batteriesRespawnTime);

                _batteriesSpawner.ActivateRandomBatteries();

                yield return new WaitForSeconds(_batteriesLifeTime);
            }
        }

        private void CheckLighthouseState(int currentItems)
        {
            if (currentItems == _notesToChangeLighthouseMode)
            {
                _lighthouse.SetRotationMode(Lighthouse.RotationMode.Target);
            }
        }

        private void OnChangeAmbience(Ambience ambience)
        {
            switch (ambience)
            {
                case Forest:
                    _ambienceAudioSource.clip = _forestAmbienceAudioClip;
                    _ambienceAudioSource.Play();
                    break;
                case Ambience.Cave:
                    _ambienceAudioSource.clip = _caveAmbienceAudioClip;
                    _ambienceAudioSource.Play();
                    break;
            }
        }
    }
}