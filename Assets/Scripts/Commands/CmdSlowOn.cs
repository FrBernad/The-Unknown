using Controllers;
using Strategy;

namespace Commands
{
    public class CmdSlowOn : ICommand
    {
        private readonly MovementController _movementController;

        public CmdSlowOn(MovementController movementController)
        {
            _movementController = movementController;
        }

        public void Execute()
        {
            _movementController.Slow(true);
        }
    }
}