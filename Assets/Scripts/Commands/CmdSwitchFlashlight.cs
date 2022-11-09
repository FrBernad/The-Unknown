using Interfaces;

namespace Commands
{
    public class CmdSwitchFlashlight : ICommand
    {
        private readonly Flashlight _flashlight;

        public CmdSwitchFlashlight(Flashlight flashlight)
        {
            _flashlight = flashlight;
        }

        public void Execute()
        {
            _flashlight.Switch();
        }
    }
}