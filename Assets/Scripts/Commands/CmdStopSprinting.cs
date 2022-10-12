using Controllers;
using Interfaces;

namespace Commands
{
    public class CmdStopSprinting : ICommand
    {
        private LifeController _lifeController;
        private MovementController _movementController;
        private bool _sprint;

        public CmdStopSprinting(LifeController lifeController, MovementController movementController)
        {
            _lifeController = lifeController;
            _movementController = movementController;
            _sprint = false;
        }

        public void Execute()
        {
            _movementController.Sprint(false);
        }
    }
}