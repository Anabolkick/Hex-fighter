using System;

namespace Attributable.Attributes
{
    public interface IAttribute
    {
        event Action<IAttribute> OnChanged;

        IAttribute GetInstance();
        
        void SetInstance(IAttribute attribute);

        IAttribute CopyInstance();
    }
}