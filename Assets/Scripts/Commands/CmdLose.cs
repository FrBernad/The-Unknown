using Interfaces;
using Managers;
using UnityEngine;

namespace Commands
{
    public class CmdLose : ICommand
    {
        private readonly GameObject _screamer;

        public void Execute()
        {
            EventManager.instance.GameOver(false);
        }
    }
}