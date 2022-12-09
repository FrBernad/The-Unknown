using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

namespace Entities.Note
{
    public class NotesSpawner : MonoBehaviour
    {
        private readonly Dictionary<string, int> _notesAmountPerZone = new Dictionary<string, int>
        {
            { "Lighthouse", 1 },
            { "House", 1 },
            { "BackLeft", 1 },
            { "Center", 2 },
            { "Cave", 2 },
            { "Mountain", 2 },
            { "Right", 1 }
        };

        private readonly Dictionary<string, List<GameObject>> _notesPerZone = new Dictionary<string, List<GameObject>>
        {
            { "Lighthouse", new List<GameObject>() },
            { "House", new List<GameObject>() },
            { "BackLeft", new List<GameObject>() },
            { "Center", new List<GameObject>() },
            { "Cave", new List<GameObject>() },
            { "Mountain", new List<GameObject>() },
            { "Right", new List<GameObject>() }
        };

        private void Awake()
        {
            Assert.AreEqual(FindObjectOfType<Inventory>().GetMaxItems(), _notesAmountPerZone.Values.Sum());
            SetAndDeactivateNotes();
        }

        private void Start()
        {
            ActivateRandomNotes();
        }

        private void SetAndDeactivateNotes()
        {
            var spawnPoints = GameObject.Find("NotesSpawnPoints");

            foreach (Transform zone in spawnPoints.transform)
            foreach (Transform point in zone)
            {
                var obj = point.gameObject;

                obj.SetActive(false);
                _notesPerZone[zone.name].Add(obj);
            }
        }

        private void ActivateRandomNotes()
        {
            foreach (var zone in _notesAmountPerZone)
                for (var i = 0; i < zone.Value; i++)
                {
                    var count = _notesPerZone[zone.Key].Count;
                    var notes = _notesPerZone[zone.Key];
                    var randomNote = Random.Range(0, count);
                    notes[randomNote].SetActive(true);
                    notes.RemoveAt(randomNote);
                }
        }
    }
}