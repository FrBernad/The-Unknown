using Interfaces;
using UnityEngine;

namespace Commands
{
    public class CmdRotation : ICommand
    {
        private readonly Vector3 _direction;
        private readonly IMovable _movable;

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