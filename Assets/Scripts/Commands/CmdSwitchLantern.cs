using Interfaces;

namespace Commands
{
    public class CmdSwitchLantern : ICommand
    {
        private Flashlight _flashlight;

        public CmdSwitchLantern(Flashlight flashlight)
        {
            _flashlight = flashlight;
        }

        public void Execute()
        {
            _flashlight.Switch();
        }
    }
}