using Managers;
using Strategy;
using UnityEngine;

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