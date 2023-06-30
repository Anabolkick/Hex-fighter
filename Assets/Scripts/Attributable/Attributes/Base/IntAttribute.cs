using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Attributable.Attributes
{
    [Serializable]
    public class IntAttribute : IAttribute
    {
#if  UNITY_EDITOR
        [OnValueChanged("OnValueChangedHandler")]
#endif
        [SerializeField] 
        protected int _value;

        [SerializeField] 
        protected int _minValue;
        
        [SerializeField] 
        protected int _maxValue;
        
        public int Value => _value;

        public event Action<IAttribute> OnChanged;

        public event Action<int> OnValueChanged;

#if UNITY_EDITOR
        protected void OnValueChangedHandler(int value)
        {
            OnChanged?.Invoke(this);
            OnValueChanged?.Invoke(value);
        }
#endif

        public IAttribute GetInstance()
        {
            return this;
        }
        
        public void SetInstance(IAttribute attribute)
        {
            if (attribute is IntAttribute floatAttribute)
            {
                SetValue(floatAttribute._value);
            }
        }
        
        public IAttribute CopyInstance()
        {
            var tempInstance = new IntAttribute();
            
            tempInstance.SetInstance(this);

            return tempInstance;
        }
        
        public void SetValue(int value)
        {
            if (_value != value)
            {
                _value = value;

                if (_value < _minValue) _value = _minValue;

                if (_value > _maxValue) _value = _maxValue;
                
                OnChanged?.Invoke(this);

                OnValueChanged?.Invoke(_value);
            }
        }

        public void IncreaseValue(int value)
        {
            SetValue(_value + value);
        }

        public void DecreaseValue(int value)
        {
            SetValue(_value - value);
        }

        public override bool Equals(object obj)
        {
            return obj is IntAttribute;
        }

        public override int GetHashCode()
        {
            return nameof(IntAttribute).GetHashCode();
        }
    }
}