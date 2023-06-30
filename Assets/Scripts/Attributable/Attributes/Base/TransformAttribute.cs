using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Attributable.Attributes
{
    public class TransformAttribute : IAttribute
    {
#if  UNITY_EDITOR
        [OnValueChanged("OnValueChangedHandler")]
#endif
        [SerializeField] 
        protected Transform _value;

        public Transform Value => _value;

        public event Action<IAttribute> OnChanged;

        public event Action<Transform> OnValueChanged;
        
#if UNITY_EDITOR
        protected void OnValueChangedHandler(Transform value)
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
            var tempInstance = new TransformAttribute();

            tempInstance.SetInstance(this);

            return tempInstance;
        }

        public override bool Equals(object obj)
        {
            return obj is TransformAttribute;
        }

        public override int GetHashCode()
        {
            return nameof(TransformAttribute).GetHashCode();
        }
    }
}