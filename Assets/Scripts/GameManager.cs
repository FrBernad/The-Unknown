using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _monster;
    [SerializeField] private GameObject _player;
    [SerializeField] private List<Transform> _spawnPoints;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _monsterSpawnClip;

    [SerializeField] private int _monsterRespawnTime = 10;
    [SerializeField] private int _monsterLifeTime = 10;

    private void Start()
    {
        SetSpawnPoints();
        SetSpawnPosition(_player);
        StartCoroutine(MonsterLifecycle());
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
        GameObject spawnPoints = GameObject.Find("SpawnPoints");
        int totalSpawnPoints = spawnPoints.transform.childCount;

        _spawnPoints = new List<Transform>(totalSpawnPoints);

        for (int i = 0; i < totalSpawnPoints; i++)
        {
            _spawnPoints.Add(spawnPoints.transform.GetChild(i));
        }
    }

    private void SetSpawnPosition(GameObject obj)
    {
        int spawnPoint = Random.Range(0, _spawnPoints.Count);
        obj.transform.position = _spawnPoints[spawnPoint].position;
    }

    public GameObject SpawnMonster()
    {
        GameObject monster = Instantiate(_monster);
        SetSpawnPosition(_monster);
        _audioSource.PlayOneShot(_monsterSpawnClip);
        return monster;
    }

    //     public void Botones(bool eleccion)
    // {
    //     if (eleccion == true)
    //     {
    //         SceneManager.LoadScene(0);
    //     }
    //     else
    //     {
    //         Application.Quit();
    //     }
    // }
    //
    //
    // public void Aparecefinal(string texto)
    // {
    //     panelFinal.SetActive(true);
    //     textoFinal.text = texto;
    //
    // }
}