using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _monster;
    [SerializeField] private List<Transform> _entitiesSpawnPoints;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _monsterSpawnClip;

    [SerializeField] private int _monsterRespawnTime = 10;
    [SerializeField] private int _monsterLifeTime = 30;

    private void Start()
    {
        SetSpawnPoints();
        StartCoroutine(MonsterLifecycle());
        EventManager.instance.OnGameOver += GameOver;
        // StartCoroutine(DisplayInitialMessage());
    }

    private IEnumerator<WaitForSeconds> DisplayInitialMessage()
    {
        EventManager.instance.UIPanelUpdate(true, "Grab all the notes and \n return to the boat");
        yield return new WaitForSeconds(2);
        EventManager.instance.UIPanelUpdate(false, "");
    }

    private IEnumerator<WaitForSeconds> MonsterLifecycle()
    {
        while (true)
        {
            yield return new WaitForSeconds(_monsterRespawnTime);

            GameObject monster = SpawnMonster();

            yield return new WaitForSeconds(_monsterLifeTime);

            Destroy(monster);
        }
    }

    private void SetSpawnPoints()
    {
        GameObject spawnPoints = GameObject.Find("EntitiesSpawnPoints");
        int totalSpawnPoints = spawnPoints.transform.childCount;

        _entitiesSpawnPoints = new List<Transform>(totalSpawnPoints);

        for (int i = 0; i < totalSpawnPoints; i++)
        {
            _entitiesSpawnPoints.Add(spawnPoints.transform.GetChild(i));
        }
    }

    private void SetSpawnPosition(GameObject obj)
    {
        int spawnPoint = Random.Range(0, _entitiesSpawnPoints.Count);
        obj.transform.position = _entitiesSpawnPoints[spawnPoint].position;
    }

    public GameObject SpawnMonster()
    {
        GameObject monster = Instantiate(_monster);
        SetSpawnPosition(_monster);
        _audioSource.PlayOneShot(_monsterSpawnClip);
        return monster;
    }
    public void GameOver(bool isVictory)
    {
        GlobalData.instance.SetVictoryField(isVictory);
        SceneManager.LoadScene("Game Over");
    }
}