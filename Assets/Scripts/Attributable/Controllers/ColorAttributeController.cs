using Attributable.Attributes;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Attributable.Controllers
{
    public class ColorAttributeController : SerializedMonoBehaviour
    {
        [OdinSerialize] private IAttributable _attributable;
        [SerializeField] private Color _color;
        
        public void SetColorAttribute()
        { 
            _attributable.GetAttribute<DynamicsTag, ColorAttribute>().SetValue(_color);
        }
    }
}