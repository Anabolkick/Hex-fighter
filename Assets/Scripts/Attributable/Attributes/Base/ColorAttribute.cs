using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Attributable.Attributes
{
    [Serializable]
    public class ColorAttribute : IAttribute
    {
#if  UNITY_EDITOR
        [OnValueChanged("OnValueChangedHandler")]
#endif
        [SerializeField] 
        protected Color _value;
        
        public Color Value => _value;
        
        public event Action<IAttribute> OnChanged;

        public event Action<Color> OnValueChanged;

#if UNITY_EDITOR
        protected void OnValueChangedHandler(Color value)
        {
            OnChanged?.Invoke(this);
            OnValueChanged?.Invoke(value);
        }
#endif
        
        public IAttribute GetInstance()
        {
            return this;
        }

        public void SetValue(Color value)
        {
            _value = value;
            
            OnValueChanged?.Invoke(_value);
        }

        public void SetInstance(IAttribute attribute)
        {
            if (attribute is ColorAttribute colorAttribute)
            {
                if (_value != colorAttribute.Value)
                {
                    _value = colorAttribute.Value;

                    OnChanged?.Invoke(this);
                    
                    OnValueChanged?.Invoke(_value);
                }
            }
        }
        
        public IAttribute CopyInstance()
        {
            var tempInstance = new ColorAttribute();
            
            tempInstance.SetInstance(this);

            return tempInstance;
        }
        
        public override bool Equals(object obj)
        {
            return obj is ColorAttribute;
        }

        public override int GetHashCode()
        {
            return nameof(ColorAttribute).GetHashCode();
        }
    }
}