using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Attributable.Attributes
{
    [Serializable]
    public class StringAttribute : IAttribute
    {
#if  UNITY_EDITOR
        [OnValueChanged("OnValueChangedHandler")]
#endif
        [SerializeField] 
        protected string _value;
        
        public string Value => _value;
        
        public event Action<IAttribute> OnChanged;
        
        public event Action<string> OnValueChanged;
        
#if UNITY_EDITOR
        protected void OnValueChangedHandler(string value)
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
            if (attribute is StringAttribute newMesh)
            {
                SetValue(newMesh._value);
            }
        }

        public virtual IAttribute CopyInstance()
        {
            var tempInstance = new StringAttribute();
            
            tempInstance.SetInstance(this);

            return tempInstance;
        }
        
        public virtual void SetValue(string value)
        {
            if (string.IsNullOrEmpty(_value) || !_value.Equals(value))
            {
                _value = value;
             
                OnChanged?.Invoke(this);
             
                OnValueChanged?.Invoke(_value);
            }
        }

        public override bool Equals(object obj)
        {
            return obj is StringAttribute;
        }

        public override int GetHashCode()
        {
            return nameof(StringAttribute).GetHashCode();
        }
    }
}