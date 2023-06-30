using Hexes;

namespace General.Signals
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