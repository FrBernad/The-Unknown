using Controllers;
using Strategy;

namespace Commands
{
    public class CmdStopSprinting : ICommand
    {
        private readonly MovementController _movementController;

        public CmdStopSprinting(MovementController movementController)
        {
            _movementController = movementController;
        }

        public void Execute()
        {
            _movementController.Sprint(false);
        }
    }
}