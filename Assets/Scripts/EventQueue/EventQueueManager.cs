using Interfaces;

namespace EventQueues
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class EventQueueManager : MonoBehaviour
    {
        static public EventQueueManager instance;

        public Queue<ICommand> Events => _events;
        private Queue<ICommand> _events = new Queue<ICommand>();

        public Queue<ICommand> MovementEvents => _events;
        private Queue<ICommand> _movementEvents = new Queue<ICommand>();

        [SerializeField] private bool _isPlayerFrozen;

        private void Awake()
        {
            if (instance != null) Destroy(this);
            instance = this;
        }

        private void Update()
        {
            while (!IsQueueEmpty())
                _events.Dequeue().Execute();

            while (!IsMoveQueueEmpty())
            {
                if (!_isPlayerFrozen)
                    _movementEvents.Dequeue().Execute();
            }

            _movementEvents.Clear();
        }

        public void AddCommand(ICommand command) => _events.Enqueue(command);
        public void AddMovementCommand(ICommand command) => _movementEvents.Enqueue(command);

        private bool IsQueueEmpty() => _events.Count <= 0;
        private bool IsMoveQueueEmpty() => _movementEvents.Count <= 0;
    }
}