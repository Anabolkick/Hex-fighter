using System;

namespace Attributable.Attributes
{
    public class SelectedAttribute : IAttribute
    {
        public event Action<IAttribute> OnChanged;
        
        public IAttribute GetInstance()
        {
            return this;
        }

        public void SetInstance(IAttribute attribute)
        {
            
        }

        public IAttribute CopyInstance()
        {
            return new SelectedAttribute();
        }
        
        public override bool Equals(object obj)
        {
            return obj is SelectedAttribute;
        }

        public override int GetHashCode()
        {
            return nameof(SelectedAttribute).GetHashCode();
        }
    }
}