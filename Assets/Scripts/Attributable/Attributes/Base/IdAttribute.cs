namespace Attributable.Attributes
{
    public class IdAttribute : StringAttribute
    {
        public override IAttribute CopyInstance()
        {
            var tempInstance = new IdAttribute();
            
            tempInstance.SetInstance(this);

            return tempInstance;
        }

        public override bool Equals(object obj)
        {
            return obj is IdAttribute;
        }

        public override int GetHashCode()
        {
            return nameof(IdAttribute).GetHashCode();
        }
    }
}