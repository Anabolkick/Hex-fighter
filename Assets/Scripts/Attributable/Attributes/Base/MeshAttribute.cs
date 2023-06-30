using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Attributable.Attributes
{
    [Serializable]
    public class MeshAttribute : IAttribute
    {
#if  UNITY_EDITOR
        [OnValueChanged("OnValueChangedHandler")]
#endif
        [SerializeField] 
        protected Mesh _value;
        
        public Mesh Value => _value;
        
        public event Action<IAttribute> OnChanged;
        
        public event Action<Mesh> OnValueChanged;

#if UNITY_EDITOR
        protected void OnValueChangedHandler(Mesh value)
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
            if (attribute is MeshAttribute meshAttribute)
            {
                if (_value != meshAttribute.Value)
                {
                    _value = meshAttribute.Value;

                    OnChanged?.Invoke(this);

                    OnValueChanged?.Invoke(_value);
                }
            }
        }
        
        public IAttribute CopyInstance()
        {
            var tempInstance = new MeshAttribute();
            
            tempInstance.SetInstance(this);

            return tempInstance;
        }

        public override bool Equals(object obj)
        {
            return obj is MeshAttribute;
        }

        public override int GetHashCode()
        {
            return nameof(MeshAttribute).GetHashCode();
        }
    }
}