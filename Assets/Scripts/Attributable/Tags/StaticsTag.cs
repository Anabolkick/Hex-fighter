namespace Attributable
{
    public class StaticsTag : ITag
    {
        public override bool Equals(object obj)
        {
            return obj is StaticsTag;
        }

        public override int GetHashCode()
        {
            return nameof(StaticsTag).GetHashCode();
        }
    }
}