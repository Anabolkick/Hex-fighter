using Units;

namespace General.EventBus
{
    public class UnitSelectedSignal
    {
        public UnitSelectedSignal(Unit unit)
        {
            Unit = unit;
        }
        
        public readonly Unit Unit;
    }
}