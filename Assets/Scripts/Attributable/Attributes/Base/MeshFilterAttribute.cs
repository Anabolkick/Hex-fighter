using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Attributable.Attributes
{
    [Serializable]
    public class MeshFilterAttribute : IAttribute
    {
#if  UNITY_EDITOR
        [OnValueChanged("OnValueChangedHandler")]
#endif
        [SerializeField]
        protected MeshFilter _value;

        public MeshFilter Value => _value;
        
        public event Action<IAttribute> OnChanged;
        
        public event Action<MeshFilter> OnValueChanged;
        
#if UNITY_EDITOR
        protected void OnValueChangedHandler(MeshFilter value)
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
            var tempInstance = new MeshFilterAttribute();
            
            tempInstance.SetInstance(this);

            return tempInstance;
        }

        public override bool Equals(object obj)
        {
            return obj is MeshFilterAttribute;
        }

        public override int GetHashCode()
        {
            return nameof(MeshFilterAttribute).GetHashCode();
        }
    }
}