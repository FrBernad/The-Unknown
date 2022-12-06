using Managers;

namespace Strategy
{
    public interface IRechargeable
    {
        public void DecreaseCharge();
        public void IncreaseCharge();
    }

    public enum ChargeStatus
    {
        Empty = 0,
        QuarterCharge,
        HalfCharge,
        ThreeQuartersCharge,
        FullCharge
    }
}