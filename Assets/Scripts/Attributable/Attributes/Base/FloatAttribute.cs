using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Attributable.Attributes
{
    [Serializable]
    public class FloatAttribute : IAttribute
    {
#if  UNITY_EDITOR
        [OnValueChanged("OnValueChangedHandler")]
#endif
        [SerializeField] 
        protected float _value;
        
        public float Value => _value;

        public event Action<IAttribute> OnChanged;

        public virtual event Action<float> OnValueChanged;
        
#if UNITY_EDITOR
        protected void OnValueChangedHandler(float value)
        {
            OnChanged?.Invoke(this);
            OnValueChanged?.Invoke(value);
        }
#endif

        public virtual IAttribute GetInstance()
        {
            return this;
        }
        
        public virtual void SetInstance(IAttribute attribute)
        {
            if (attribute is FloatAttribute floatAttribute)
            {
                SetValue(floatAttribute._value);
            }
        }
        
        public virtual IAttribute CopyInstance()
        {
            var tempInstance = new FloatAttribute();
            
            tempInstance.SetInstance(this);

            return tempInstance;
        }
        
        public virtual void SetValue(float value)
        {
            if (_value != value)
            {
                _value = value;

                OnChanged?.Invoke(this);
                
                OnValueChanged?.Invoke(_value);
            }
        }
        
        public virtual void IncreaseValue(float value)
        {
            SetValue(_value + value);
        }

        public virtual void DecreaseValue(float value)
        {
            SetValue(_value - value);
        }

        public override bool Equals(object obj)
        {
            return obj is FloatAttribute;
        }

        public override int GetHashCode()
        {
            return nameof(FloatAttribute).GetHashCode();
        }
    }
}