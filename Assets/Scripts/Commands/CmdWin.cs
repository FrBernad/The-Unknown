using Managers;
using Strategy;

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