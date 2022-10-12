using Interfaces;
using UnityEngine;

namespace Commands
{
    public class CmdRotation : ICommand
    {
        private IMovable _movable;
        private Vector3 _direction;

        public CmdRotation(IMovable movable, Vector3 direction)
        {
            _movable = movable;
            _direction = direction;
        }

        public void Execute()
        {
            _movable.Rotate(_direction);
        }
    }
}