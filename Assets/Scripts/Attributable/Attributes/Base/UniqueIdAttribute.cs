using System;
using Sirenix.OdinInspector;

namespace Attributable.Attributes
{
    public class UniqueIdAttribute : StringAttribute
    {
        public UniqueIdAttribute()
        {
            _value = Guid.NewGuid().ToString();
        }
        
        public UniqueIdAttribute(string id)
        {
            _value = id;
        }

        
        [Button]
        private void UpdateGUID()
        {
            _value = Guid.NewGuid().ToString();
        }

        public override bool Equals(object obj)
        {
            return obj is UniqueIdAttribute;
        }

        public override int GetHashCode()
        {
            return nameof(UniqueIdAttribute).GetHashCode();
        }
    }
}