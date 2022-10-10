using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityRandom = UnityEngine.Random;
using SystemRandom = System.Random;

public class NotesSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _cubePrefab;
    [SerializeField] private List<Transform> _spawnPoints;

    [SerializeField] private int _totalNotes = 4;


    private void Awake()
    {
        SetSpawnPoints();
    }

    private void Start()
    {
        for (var i = 0; i < _totalNotes; i++)
        {
            var index = Random.Range(0, _spawnPoints.Count);
            Instantiate(_cubePrefab, _spawnPoints[index].position, Quaternion.identity);
            _spawnPoints.RemoveAt(index);
        }
    }

    private void SetSpawnPoints()
    {
        GameObject spawnPoints = GameObject.Find("NotesSpawnPoints");
        int totalSpawnPoints = spawnPoints.transform.childCount;

        _spawnPoints = new List<Transform>(totalSpawnPoints);

        for (int i = 0; i < totalSpawnPoints; i++)
        {
            _spawnPoints.Add(spawnPoints.transform.GetChild(i));
        }
    }
}