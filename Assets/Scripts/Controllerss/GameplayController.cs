using Attributable;
using Attributable.Attributes;
using General.EventBus;
using Hexes;
using Sirenix.OdinInspector;
using UnityEngine;
using VContainer;
using IInitializable = General.Initialize.IInitializable;
using Unit = Units.Unit;

namespace Controllers
{
    public class GameplayController : MonoBehaviour, IInitializable
    {
        [SerializeField, ReadOnly] private HexCell _selectedHex;
        [SerializeField, ReadOnly] private Unit _selectedUnit;
        
        private EventBus _eventBus;

        [Inject]
        private void Construct(EventBus eventBus)
        {
            _eventBus = eventBus;
        }
        
        public void Initialize()
        {
            _eventBus.Subscribe<HexSelectedSignal>(OnHexSelectedHandler);
        }
        
        private void OnDisable()
        {
            _eventBus.Unsubscribe<HexSelectedSignal>(OnHexSelectedHandler);
        }
        
        private void OnHexSelectedHandler(HexSelectedSignal hexSelectedSignal)
        {
            if (_selectedHex != null && _selectedHex.ContainAttribute<DynamicsTag, SelectedAttribute>())
            {
                _selectedHex.RemoveAttribute<DynamicsTag, SelectedAttribute>();

                _selectedUnit = null;
            }
            
            _selectedHex = hexSelectedSignal.Hex;
            _selectedUnit = _selectedHex.GetAttribute<DynamicsTag, UnitAttribute>().Value;
            
            if(_selectedUnit!=null) Debug.Log(_selectedUnit.GetAttribute<StaticsTag, IdAttribute>().Value);
        }

    }
}