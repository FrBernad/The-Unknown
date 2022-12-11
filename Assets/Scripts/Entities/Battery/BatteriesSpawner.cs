using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Entities.Battery
{
    public class BatteriesSpawner : MonoBehaviour
    {
        private readonly Dictionary<string, int> _batteriesAmountPerZone = new Dictionary<string, int>
        {
            { "Spawn", 3 },
            { "Lighthouse", 1 },
            { "House", 3 },
            { "Mountain", 3 },
            { "Cave", 3 },
            { "Center", 3 },
            { "Right", 2 }
        };

        private readonly Dictionary<string, List<GameObject>> _batteriesPerZone =
            new Dictionary<string, List<GameObject>>
            {
                { "Spawn", new List<GameObject>() },
                { "Lighthouse", new List<GameObject>() },
                { "House", new List<GameObject>() },
                { "Mountain", new List<GameObject>() },
                { "Cave", new List<GameObject>() },
                { "Center", new List<GameObject>() },
                { "Right", new List<GameObject>() }
            };

        private void Awake()
        {
            SetAndDeactivateBatteries();
        }

        private void SetAndDeactivateBatteries()
        {
            var spawnPoints = GameObject.Find("BatteriesSpawnPoints");

            foreach (Transform zone in spawnPoints.transform)
            {
                foreach (Transform point in zone)
                {
                    var obj = point.gameObject;

                    obj.SetActive(false);
                    _batteriesPerZone[zone.name].Add(obj);
                }
            }
        }

        public void ActivateRandomBatteries()
        {
            foreach (var zone in _batteriesPerZone)
            {
                foreach (var battery in zone.Value)
                {
                    battery.SetActive(false);
                }
            }

            foreach (var zone in _batteriesAmountPerZone)
            {
                var indexes = Enumerable.Range(0, _batteriesPerZone[zone.Key].Count).ToList();
                for (var i = 0; i < zone.Value; i++)
                {
                    var batteries = _batteriesPerZone[zone.Key];
                    var randomBattery = Random.Range(0, indexes.Count);
                    batteries[randomBattery].SetActive(true);
                    indexes.RemoveAt(randomBattery);
                }
            }
        }
    }
}