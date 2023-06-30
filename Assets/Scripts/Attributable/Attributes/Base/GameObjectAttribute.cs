using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Attributable.Attributes
{
    [Serializable]
    public class GameObjectAttribute : IAttribute
    {
#if  UNITY_EDITOR
        [OnValueChanged("OnValueChangedHandler")]
#endif
        [SerializeField] 
        protected GameObject _value;
        
        public GameObject Value => _value;

        public event Action<IAttribute> OnChanged;
        
        public event Action<GameObject> OnValueChanged; 
        
#if UNITY_EDITOR
        protected void OnValueChangedHandler(GameObject value)
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
            if (attribute is GameObjectAttribute containerAttribute)
            {
                if (_value != containerAttribute.Value)
                {
                    _value = containerAttribute.Value;

                    OnChanged?.Invoke(this);
                    
                    OnValueChanged?.Invoke(_value);
                }
            }
        }
        
        public IAttribute CopyInstance()
        {
            var tempInstance = new GameObjectAttribute();
            
            tempInstance.SetInstance(this);

            return tempInstance;
        }
        
        public override bool Equals(object obj)
        {
            return obj is GameObjectAttribute;
        }

        public override int GetHashCode()
        {
            return nameof(GameObjectAttribute).GetHashCode();
        }
    }
}