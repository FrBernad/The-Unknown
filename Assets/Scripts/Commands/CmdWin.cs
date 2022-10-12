using Interfaces;
using Managers;

namespace Commands
{
    public class CmdWin : ICommand
    {
        public void Execute()
        {
            EventManager.instance.GameOver(true);
            EventManager.instance.UIPanelUpdate(null);
        }
    }
}