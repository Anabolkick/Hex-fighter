using System;
using Hexes;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Attributable.Attributes
{
    public class HexAttribute : IAttribute
    {
        [SerializeField] private HexCell _value;
        
        public HexCell Value => _value;
        
        public event Action<IAttribute> OnChanged;
        public virtual event Action<HexCell> OnValueChanged;

        [Button]
        public void SetValue(HexCell value)
        {
            _value = value;
            OnValueChanged?.Invoke(_value);
        }

        public IAttribute GetInstance()
        {
            return this;
        }

        public void SetInstance(IAttribute attribute)
        {
            if (attribute is HexAttribute hexAttribute)
            {
                _value = hexAttribute._value;
                OnChanged?.Invoke(this);
                OnValueChanged?.Invoke(_value);
            }
        }

        public IAttribute CopyInstance()
        {
            var tempInstance = new BoolAttribute();

            tempInstance.SetInstance(this);

            return tempInstance;
        }
    }
}