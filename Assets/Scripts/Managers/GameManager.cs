using System.Collections.Generic;
using Entities;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameObject _monster;
        [SerializeField] private List<Transform> _entitiesSpawnPoints;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _monsterSpawnClip;

        [SerializeField] private int _monsterRespawnTime = 10;
        [SerializeField] private int _monsterLifeTime = 30;

        [SerializeField] private AudioClip _screamerAudioClip;
        [SerializeField] private GameObject _screamerObject;

        private void Start()
        {
            SetSpawnPoints();
            StartCoroutine(MonsterLifecycle());
            EventManager.instance.OnGameOver += OnGameOver;
            EventManager.instance.OnStartConsumingBattery += OnStartConsumingBattery;
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

        public GameObject SpawnMonster()
        {
            var monster = Instantiate(_monster);
            SetSpawnPosition(_monster);
            _audioSource.PlayOneShot(_monsterSpawnClip);
            return monster;
        }

        public void OnGameOver(bool isVictory)
        {
            GlobalData.instance.SetVictoryField(isVictory);
            if (!isVictory)
            {
                _audioSource.PlayOneShot(_screamerAudioClip);
                _screamerObject.SetActive(true);
                StartCoroutine(Utils.Utils.DoDelayed(2.5f, () => SceneManager.LoadScene("Game Over")));
            }
            else
            {
                SceneManager.LoadScene("Game Over");
            }
        }

        public void OnStartConsumingBattery(bool consumeBattery)
        {
            Flashlight flashlight = GameObject.FindWithTag("Player").GetComponentInChildren<Flashlight>(true);
            flashlight.SetIsChargeable(consumeBattery);
        }
    }
}