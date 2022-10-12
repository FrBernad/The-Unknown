using Controllers;
using Interfaces;

namespace Commands
{
    public class CmdStartSprinting : ICommand
    {
        private LifeController _lifeController;
        private MovementController _movementController;
        private bool _sprint;

        public CmdStartSprinting(LifeController lifeController, MovementController movementController)
        {
            _lifeController = lifeController;
            _movementController = movementController;
            _sprint = true;
        }

        public void Execute()
        {
            if (_lifeController.CurrentStamina > 0)
            {
                _movementController.Sprint(true);
                _lifeController.decreaseStamina(0.3f);
            }
            else
            {
                _movementController.Sprint(false);
            }
        }
    }
}