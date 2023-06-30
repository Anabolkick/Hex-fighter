using Hexes;

namespace General.EventBus
{
    public class HexGridBuiltSignal
    {
        public HexGridBuiltSignal(HexGrid hexGrid)
        {
            HexGrid = hexGrid;
        }
        
        public readonly HexGrid HexGrid;
    }
}