namespace Attributable
{
    public class DynamicsTag : ITag
    {
        public override bool Equals(object obj)
        {
            return obj is DynamicsTag;
        }

        public override int GetHashCode()
        {
            return nameof(DynamicsTag).GetHashCode();
        }
    }
}