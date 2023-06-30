using System;
using System.Linq;
using Attributable;
using Attributable.Attributes;
using General.EventBus;
using General.Signals;
using Sirenix.OdinInspector;
using Units;
using UnityEngine;
using UnityEngine.EventSystems;
using VContainer;

namespace Hexes
{
    public class HexCell : AttributableMonoBehaviour
    {
        [SerializeField] private EventTrigger _eventTrigger;
        
        [ReadOnly, ShowInInspector] private Vector2 _coordinates;

        private EventBus _eventBus;
        
        public Vector2 Coordinates => _coordinates;
        
        [Inject]
        private void Construct(EventBus eventBus)
        {
            _eventBus = eventBus;
        }
        
        private void OnAttributeAddedHandler(ITag tag, IAttribute attribute)
        {
            if (attribute is SelectedAttribute)
            {
                GetAttribute<DynamicsTag, ColorAttribute>().SetValue(Color.yellow);
            }
        }
        
        private void OnAttributeRemovedHandler(ITag tag, IAttribute attribute)
        {
            if (attribute is SelectedAttribute)
            {
                GetAttribute<DynamicsTag, ColorAttribute>().SetValue(Color.blue);
            }
        }
        
        private void OnPointerClickHandler(BaseEventData eventData)
        {
            if (!ContainAttribute<DynamicsTag, SelectedAttribute>())
            {
                AddAttribute<DynamicsTag, SelectedAttribute>();
                _eventBus.Invoke(new HexSelectedSignal(this));
            }
        }
        
        public void Initialize()
        {
            var onClickEntity = new EventTrigger.Entry();
            onClickEntity.eventID = EventTriggerType.PointerClick;
            onClickEntity.callback.AddListener(OnPointerClickHandler);
            
            _eventTrigger.triggers.Add(onClickEntity);

            OnAttributeAdded += OnAttributeAddedHandler;
            OnAttributeRemoved += OnAttributeRemovedHandler;
        }
        
        public void SetCoordinates(float x, float y)
        {
            _coordinates = new Vector2(x, y);
        }
        
        private void OnDisable()
        {
            foreach (var trigger in _eventTrigger.triggers)
            {
                trigger.callback.RemoveAllListeners();
            }
            
            OnAttributeAdded -= OnAttributeAddedHandler;
            OnAttributeRemoved -= OnAttributeRemovedHandler;
        }
    }
}

