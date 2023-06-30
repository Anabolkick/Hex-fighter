using Attributable.Attributes;
using Hexes;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using UnityEngine.UI;

namespace Attributable.Handlers
{
    public class HexAttributeHandler : SerializedMonoBehaviour
    {
        [OdinSerialize] private AttributableMonoBehaviour _attributable;
        
        private void OnAttributeValueChangedHandler(HexCell hex)
        {
            _attributable.transform.parent = hex.transform;
            _attributable.transform.localPosition = Vector3.zero;
        }
        
        private void OnEnable()
        {
            _attributable.GetAttribute<DynamicsTag, HexAttribute>().OnValueChanged += OnAttributeValueChangedHandler;
        }

        
        private void OnDisable()
        {
            _attributable.GetAttribute<DynamicsTag, HexAttribute>().OnValueChanged -= OnAttributeValueChangedHandler;
        }
    }
}