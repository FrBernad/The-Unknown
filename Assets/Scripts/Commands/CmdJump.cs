using Interfaces;
using UnityEngine;

namespace Commands
{
    public class CmdJump : ICommand
    {
        private IMovable _movable;
        private Vector3 _direction;

        public CmdJump(IMovable movable, Vector3 direction)
        {
            _movable = movable;
            _direction = direction;
        }

        public void Execute()
        {
            _movable.Jump(_direction);
        }
    }
}