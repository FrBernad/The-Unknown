using Entities;
using Strategy;

namespace Commands
{
    public class CmdIncreaseCharge : ICommand
    {
        private readonly IRechargeable _rechargeable;

        public CmdIncreaseCharge(IRechargeable rechargeable)
        {
            _rechargeable = rechargeable;
        }

        public void Execute()
        {
            _rechargeable.IncreaseCharge();
        }
    }
}