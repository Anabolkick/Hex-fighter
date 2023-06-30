using Attributable;
using Attributable.Attributes;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Units
{
    public class Unit : AttributableMonoBehaviour, IPointerDownHandler 
    {
        protected virtual void OnEnable()
        {
            var hex = GetAttribute<DynamicsTag, HexAttribute>().Value;
            
            if (hex != null)
            {
                transform.parent = hex.transform;
                transform.localPosition = Vector3.zero;
            }
        }

        public virtual void OnPointerDown(PointerEventData eventData)
        {
            var selectedAttribute = GetAttribute<DynamicsTag, SelectedAttribute>();

            if (selectedAttribute != null)
            {
                // Если юнит уже выбран, снимаем выделение
                RemoveAttribute<DynamicsTag, SelectedAttribute>();
            }
            else
            {
                // Если юнит не выбран, устанавливаем атрибут выбранного объекта
                AddAttribute<DynamicsTag, SelectedAttribute>();
            }
        }
    }
}
