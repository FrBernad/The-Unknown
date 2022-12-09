using System;
using UnityEngine;

namespace Entities.Cave
{
    public class Cave : MonoBehaviour
    {
        [SerializeField] private GameObject _closedEntrance;
        [SerializeField] private GameObject _openedEntrance;
        public bool isOpen;

        private void Start()
        {
            _closedEntrance.SetActive(true);
            _openedEntrance.SetActive(false);
        }

        public void Open()
        {
            isOpen = true;
            _closedEntrance.SetActive(false);
            _openedEntrance.SetActive(true);
        }
    }
}