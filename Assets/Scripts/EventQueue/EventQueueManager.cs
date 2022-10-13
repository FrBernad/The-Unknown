using System.Collections.Generic;
using Interfaces;
using UnityEngine;

namespace EventQueues
{
    public class EventQueueManager : MonoBehaviour
    {
        public static EventQueueManager instance;

        [SerializeField] private bool _isPlayerFrozen;
        private readonly Queue<ICommand> _movementEvents = new Queue<ICommand>();

        public Queue<ICommand> Events { get; } = new Queue<ICommand>();

        public Queue<ICommand> MovementEvents => Events;

        private void Awake()
        {
            if (instance != null) Destroy(this);
            instance = this;
        }

        private void Update()
        {
            while (!IsQueueEmpty())
                Events.Dequeue().Execute();

            while (!IsMoveQueueEmpty())
                if (!_isPlayerFrozen)
                    _movementEvents.Dequeue().Execute();

            _movementEvents.Clear();
        }

        public void AddCommand(ICommand command)
        {
            Events.Enqueue(command);
        }

        public void AddMovementCommand(ICommand command)
        {
            _movementEvents.Enqueue(command);
        }

        private bool IsQueueEmpty()
        {
            return Events.Count <= 0;
        }

        private bool IsMoveQueueEmpty()
        {
            return _movementEvents.Count <= 0;
        }
    }
}