using Controllers;
using Interfaces;

namespace Commands
{
    public class CmdStartSprinting : ICommand
    {
        private readonly LifeController _lifeController;
        private readonly MovementController _movementController;

        public CmdStartSprinting(LifeController lifeController, MovementController movementController)
        {
            _lifeController = lifeController;
            _movementController = movementController;
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