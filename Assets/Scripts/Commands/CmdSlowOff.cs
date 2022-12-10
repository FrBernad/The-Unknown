using Controllers;
using Strategy;

namespace Commands
{
    public class CmdSlowOff : ICommand
    {
        private readonly MovementController _movementController;

        public CmdSlowOff(MovementController movementController)
        {
            _movementController = movementController;
        }

        public void Execute()
        {
            _movementController.Slow(false);
        }
    }
}