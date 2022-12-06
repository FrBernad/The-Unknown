using Strategy;

namespace Commands
{
    public class CmdDecreaseCharge
    {
        private readonly IRechargeable _rechargeable;

        public CmdDecreaseCharge(IRechargeable rechargeable)
        {
            _rechargeable = rechargeable;
        }

        public void Execute()
        {
            _rechargeable.IncreaseCharge();
        }
    }
}