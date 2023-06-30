using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Attributable.Attributes
{
    [Serializable]
    public class SpriteAttribute : IAttribute
    {
#if  UNITY_EDITOR
        [OnValueChanged("OnValueChangedHandler")]
#endif
        [SerializeField, PreviewField] 
        protected Sprite _value;

        public Sprite Value => _value;

        public event Action<IAttribute> OnChanged;
        
        public event Action<Sprite> OnValueChanged;

#if UNITY_EDITOR
        protected void OnValueChangedHandler(Sprite value)
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
            if (attribute is SpriteAttribute spriteAttribute)
            {
                if (_value != spriteAttribute.Value)
                {
                    _value = spriteAttribute.Value;

                    OnChanged?.Invoke(this);
                    
                    OnValueChanged?.Invoke(_value);
                }
            }
        }
        
        public IAttribute CopyInstance()
        {
            var tempInstance = new SpriteAttribute();
            
            tempInstance.SetInstance(this);

            return tempInstance;
        }

        public override bool Equals(object obj)
        {
            return obj is SpriteAttribute;
        }

        public override int GetHashCode()
        {
            return nameof(SpriteAttribute).GetHashCode();
        }
    }
}