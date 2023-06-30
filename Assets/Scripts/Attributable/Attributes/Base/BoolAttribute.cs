using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Attributable.Attributes
{
    [Serializable]
    public class BoolAttribute : IAttribute
    {
#if  UNITY_EDITOR
        [OnValueChanged("OnValueChangedHandler")]
#endif
        [SerializeField]
        protected bool _value;

        public bool Value => _value;

        public event Action<IAttribute> OnChanged;
        
        public event Action<bool> OnValueChanged;
        
#if UNITY_EDITOR
        protected void OnValueChangedHandler(bool value)
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
            if (attribute is BoolAttribute boolAttribute)
            {
                if (_value != boolAttribute.Value)
                {
                    SetValue(boolAttribute.Value);
                }
            }
        }

        public virtual IAttribute CopyInstance()
        {
            var tempInstance = new BoolAttribute();

            tempInstance.SetInstance(this);

            return tempInstance;
        }
        
        public virtual void SetValue(bool value)
        {
            _value = value;

            OnChanged?.Invoke(this);

            OnValueChanged?.Invoke(_value);
        }

        public override bool Equals(object obj)
        {
            return obj is BoolAttribute;
        }

        public override int GetHashCode()
        {
            return nameof(BoolAttribute).GetHashCode();
        }
    }
}