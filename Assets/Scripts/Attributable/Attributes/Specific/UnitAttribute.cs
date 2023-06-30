using System;
using Sirenix.OdinInspector;
using Units;
using UnityEngine;

namespace Attributable.Attributes
{
    public class UnitAttribute : IAttribute
    {
        [SerializeField] private Unit _value;
        
        public Unit Value => _value;
        
        public event Action<IAttribute> OnChanged;
        public virtual event Action<Unit> OnValueChanged;

        [Button]
        public void SetValue(Unit value)
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
            if (attribute is UnitAttribute hexAttribute)
            {
                _value = hexAttribute._value;
                OnChanged?.Invoke(this);
                OnValueChanged?.Invoke(_value);
            }
        }

        public IAttribute CopyInstance()
        {
            var tempInstance = new UnitAttribute();

            tempInstance.SetInstance(this);

            return tempInstance;
        }
    }
}