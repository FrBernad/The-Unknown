using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityRandom=UnityEngine.Random;
using SystemRandom=System.Random;
public class RandomSpawner : MonoBehaviour
{
    public GameObject cubePrefab;
    public List<Vector3> noteStartPositions;

    private const int NoteAmount = 2;

    private void Awake()
    {
        noteStartPositions = new List<Vector3>()
        {
            new Vector3(UnityRandom.Range(-10, 11), 5, UnityRandom.Range(-10, 11)), 
            new Vector3(UnityRandom.Range(-10, 11), 5, UnityRandom.Range(-10, 11)),
            new Vector3(UnityRandom.Range(-10, 11), 5, UnityRandom.Range(-10, 11))
        };
        
        for (var i = 0; i < NoteAmount; i++)
        {
            var random = new SystemRandom();
            var index = random.Next(noteStartPositions.Count);
            Instantiate(cubePrefab, noteStartPositions[index], Quaternion.identity);
            noteStartPositions.RemoveAt(index);
        }
        
    }
}
