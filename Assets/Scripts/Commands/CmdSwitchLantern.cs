using Interfaces;

namespace Commands
{
    public class CmdSwitchLantern : ICommand
    {
        private readonly Flashlight _flashlight;

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