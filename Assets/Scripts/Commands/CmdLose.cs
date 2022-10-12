using Interfaces;
using Managers;

namespace Commands
{
    public class CmdLose : ICommand
    {
        public void Execute()
        {
            EventManager.instance.GameOver(false);
        }
    }
}