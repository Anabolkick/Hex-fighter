using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Attributable.Attributes
{
    [Serializable]
    public class MaterialAttribute : IAttribute
    {
#if  UNITY_EDITOR
        [OnValueChanged("OnValueChangedHandler")]
#endif
        [SerializeField] 
        protected Material _value;

        public Material Value => _value;

        public event Action<IAttribute> OnChanged;
        
        public event Action<Material> OnValueChanged;

#if UNITY_EDITOR
        protected void OnValueChangedHandler(Material value)
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
            if (attribute is MaterialAttribute materialAttribute)
            {
                if (_value != materialAttribute.Value)
                {
                    _value = materialAttribute.Value;

                    OnChanged?.Invoke(this);
                    
                    OnValueChanged?.Invoke(_value);
                }
            }
        }
        
        public IAttribute CopyInstance()
        {
            var tempInstance = new MaterialAttribute();
            
            tempInstance.SetInstance(this);

            return tempInstance;
        }
                
        public override bool Equals(object obj)
        {
            return obj is MaterialAttribute;
        }

        public override int GetHashCode()
        {
            return nameof(MaterialAttribute).GetHashCode();
        }
    }
}