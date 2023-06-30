using Hexes;

namespace General.EventBus
{
    public class HexSelectedSignal
    {
        public HexSelectedSignal(HexCell hex)
        {
            Hex = hex;
        }
        
        public readonly HexCell Hex;
    }
}