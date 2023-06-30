using Attributable.Attributes;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using UnityEngine.UI;

namespace Attributable.Handlers
{
    public class ColorAttributeHandler : SerializedMonoBehaviour
    {
        [OdinSerialize] private IAttributable _attributable;
        
        [SerializeField] private Image _image;

        private void OnAttributeValueChangedHandler(Color color)
        {
            _image.color = color;
        }
        
        private void OnEnable()
        {
            var colorAttribute = _attributable.GetAttribute<DynamicsTag, ColorAttribute>();
            
            _image.color = colorAttribute.Value;
            
            colorAttribute.OnValueChanged += OnAttributeValueChangedHandler;
        }

        
        private void OnDisable()
        {
            _attributable.GetAttribute<DynamicsTag, ColorAttribute>().OnValueChanged -= OnAttributeValueChangedHandler;
        }
    }
}