using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityRandom = UnityEngine.Random;
using SystemRandom = System.Random;

public class NotesSpawner : MonoBehaviour
{
    private Dictionary<string, int> _notesAmountPerZone = new Dictionary<string, int>
    {
        { "Lighthouse", 1 },
        { "House", 1 },
        { "BackLeft", 1 },
        { "Center", 3 },
        { "Right", 2 },
    };

    private Dictionary<string, List<GameObject>> _notesPerZone = new Dictionary<string, List<GameObject>>
    {
        { "Lighthouse", new List<GameObject>() },
        { "House", new List<GameObject>() },
        { "BackLeft", new List<GameObject>() },
        { "Center", new List<GameObject>() },
        { "Right", new List<GameObject>() },
    };

    private void Awake()
    {
        SetAndDeactivateNotes();
    }

    private void Start()
    {
        ActivateRandomNotes();
    }

    private void SetAndDeactivateNotes()
    {
        GameObject spawnPoints = GameObject.Find("NotesSpawnPoints");

        foreach (Transform zone in spawnPoints.transform)
        {
            foreach (Transform point in zone)
            {
                GameObject obj = point.gameObject;

                obj.SetActive(false);
                _notesPerZone[zone.name].Add(obj);
            }
        }
    }

    private void ActivateRandomNotes()
    {
        foreach (KeyValuePair<string, int> zone in _notesAmountPerZone)
        {
            for (int i = 0; i < zone.Value; i++)
            {
                int count = _notesPerZone[zone.Key].Count;
                List<GameObject> notes = _notesPerZone[zone.Key];
                int randomNote = Random.Range(0, count);
                notes[randomNote].SetActive(true);
                notes.RemoveAt(randomNote);
            }
        }
    }
}