using System;
using System.Collections.Generic;
using System.Linq;
using Entities;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Entities.Ambience;
using Random = UnityEngine.Random;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameObject _monster;
        [SerializeField] private List<Transform> _entitiesSpawnPoints;
        [SerializeField] private AudioSource _globalAudioSource;
        [SerializeField] private AudioClip _monsterSpawnClip;

        [SerializeField] private AudioSource _ambienceAudioSource;
        [SerializeField] private AudioClip _forestAmbienceAudioClip;
        [SerializeField] private AudioClip _caveAmbienceAudioClip;

        [SerializeField] private int _monsterRespawnTime = 10;
        [SerializeField] private int _monsterLifeTime = 30;

        [SerializeField] private AudioClip _screamerAudioClip;
        [SerializeField] private GameObject _screamerObject;

        [SerializeField] private Lighthouse _lighthouse;

        [SerializeField] private List<Transform> _batteriesSpawnPoints;
        [SerializeField] private int _batteriesRespawnTime = 20;
        [SerializeField] private int _batteriesLifeTime = 30;
        [SerializeField] private int _batteriesQuantity = 6;
        [SerializeField] private GameObject _battery;


        private void Start()
        {
            SetSpawnPoints();
            SetSpawnBatteryPoints();
            StartCoroutine(MonsterLifecycle());
            EventManager.instance.OnGameOver += OnGameOver;
            EventManager.instance.OnSetFlashlightChargeableMode += OnSetFlashlightChargeableMode;
            EventManager.instance.OnChangeAmbience += OnChangeAmbience;
            EventManager.instance.OnChangeLighthouseRotationMode += OnChangeLighthouseRotationMode;
            EventManager.instance.OnSpawnBatteries += OnSpawnBatteries;
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

                Destroy(monster);
            }
        }

        private void SetSpawnPoints()
        {
            var spawnPoints = GameObject.Find("EntitiesSpawnPoints");
            var totalSpawnPoints = spawnPoints.transform.childCount;

            _entitiesSpawnPoints = new List<Transform>(totalSpawnPoints);

            for (var i = 0; i < totalSpawnPoints; i++) _entitiesSpawnPoints.Add(spawnPoints.transform.GetChild(i));
        }

        private void SetSpawnPosition(GameObject obj)
        {
            var spawnPoint = Random.Range(0, _entitiesSpawnPoints.Count);
            obj.transform.position = _entitiesSpawnPoints[spawnPoint].position;
        }

        private GameObject SpawnMonster()
        {
            var monster = Instantiate(_monster);
            SetSpawnPosition(_monster);
            _globalAudioSource.PlayOneShot(_monsterSpawnClip);
            return monster;
        }

        private IEnumerator<WaitForSeconds> BatteriesLifecycle()
        {
            while (true)
            {
                yield return new WaitForSeconds(_batteriesRespawnTime);

                List<GameObject> batteries = SpawnBatteries();

                yield return new WaitForSeconds(_batteriesLifeTime);

                foreach (var battery in batteries)
                {
                    Destroy(battery);
                }
            }
        }

        private void SetSpawnBatteryPoints()
        {
            var spawnPoints = GameObject.Find("BatteriesSpawnPoints");
            var totalSpawnPoints = spawnPoints.transform.childCount;

            _batteriesSpawnPoints = new List<Transform>(totalSpawnPoints);

            for (var i = 0; i < totalSpawnPoints; i++) _batteriesSpawnPoints.Add(spawnPoints.transform.GetChild(i));
        }

        private List<GameObject> SpawnBatteries()
        {
            List<GameObject> batteries = new List<GameObject>();
            List<int> indexs = Enumerable.Range(0, _batteriesSpawnPoints.Count).ToList();

            for (int i = 0; i < _batteriesQuantity; i++)
            {
                var battery = Instantiate(_battery);
                int index = SetSpawnBatteryPosition(battery, indexs);
                indexs.RemoveAt(index);
                batteries.Add(battery);
            }

            return batteries;
        }

        private int SetSpawnBatteryPosition(GameObject obj, List<int> indexs)
        {
            var index = Random.Range(0, indexs.Count);
            var spawnPoint = indexs[index];
            obj.transform.position = _batteriesSpawnPoints[spawnPoint].position;
            return index;
        }


        private void OnSpawnBatteries()
        {
            StartCoroutine(BatteriesLifecycle());
        }


        private void OnGameOver(bool isVictory)
        {
            GlobalData.instance.SetVictoryField(isVictory);
            if (!isVictory)
            {
                _globalAudioSource.PlayOneShot(_screamerAudioClip);
                _screamerObject.SetActive(true);
                StartCoroutine(Utils.Utils.DoDelayed(2.5f, () => SceneManager.LoadScene("Game Over")));
            }
            else
            {
                SceneManager.LoadScene("Game Over");
            }
        }


        private void OnSetFlashlightChargeableMode(bool consumeBattery)
        {
            Flashlight flashlight = GameObject.FindWithTag("Player").GetComponentInChildren<Flashlight>(true);
            flashlight.SetIsChargeable(consumeBattery);
        }

        private void OnChangeAmbience(Ambience ambience)
        {
            switch (ambience)
            {
                case Forest:
                    _ambienceAudioSource.clip = _forestAmbienceAudioClip;
                    _ambienceAudioSource.Play();
                    break;
                case Cave:
                    _ambienceAudioSource.clip = _caveAmbienceAudioClip;
                    _ambienceAudioSource.Play();
                    break;
            }
        }

        private void OnChangeLighthouseRotationMode(Lighthouse.RotationMode rotationMode)
        {
            _lighthouse.SetRotationMode(rotationMode);
        }
    }
}