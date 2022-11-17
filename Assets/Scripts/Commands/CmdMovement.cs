using Strategy;
using UnityEngine;

namespace Commands
{
    public class CmdMovement : ICommand
    {
        private readonly Vector3 _direction;
        private readonly IMovable _movable;

        public CmdMovement(IMovable movable, Vector3 direction)
        {
            _movable = movable;
            _direction = direction;
        }

        public void Execute()
        {
            _movable.Move(_direction);
        }
    }
}