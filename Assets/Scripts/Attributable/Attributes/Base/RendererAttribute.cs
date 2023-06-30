using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Attributable.Attributes
{
    [Serializable]
    public class RendererAttribute : IAttribute
    {
#if  UNITY_EDITOR
        [OnValueChanged("OnValueChangedHandler")]
#endif
        [SerializeField] 
        protected Renderer _value;

        public Renderer Value => _value;
        
        public event Action<IAttribute> OnChanged;
        
        public event Action<Renderer> OnValueChanged;
        
#if UNITY_EDITOR
        protected void OnValueChangedHandler(Renderer value)
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
        }
        
        public IAttribute CopyInstance()
        {
            var tempInstance = new RendererAttribute();
            
            tempInstance.SetInstance(this);

            return tempInstance;
        }

        public override bool Equals(object obj)
        {
            return obj is RendererAttribute;
        }

        public override int GetHashCode()
        {
            return nameof(RendererAttribute).GetHashCode();
        }
    }
}