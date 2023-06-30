using System;
using Attributable;
using Attributable.Attributes;
using General.EventBus;
using General.Initialize;
using UnityEngine;
using VContainer;
using Random = UnityEngine.Random;

namespace Units
{
    public class Unit : AttributableMonoBehaviour, IInitializable
    {
        private EventBus _eventBus;
        
        private void OnHexGridBuiltHandler(HexGridBuiltSignal signal)
        {
            var x = Random.Range(0, signal.HexGrid.HexCells.Length);
            var y =  Random.Range(0, signal.HexGrid.HexCells[x].Length);
            var randomCell = signal.HexGrid.HexCells[x][y];
            
            GetAttribute<DynamicsTag, HexAttribute>().SetValue(randomCell);
            
            transform.parent = randomCell.transform;
            transform.localPosition = Vector3.zero;
            randomCell.GetAttribute<DynamicsTag, UnitAttribute>().SetValue(this);
        }

        [Inject]
        private void Construct(EventBus eventBus)
        {
            _eventBus = eventBus;
        }
        
        public void Initialize()
        {
            _eventBus.Subscribe<HexGridBuiltSignal>(OnHexGridBuiltHandler);
        }
        
        protected virtual void OnDisable()
        {
            _eventBus.Unsubscribe<HexGridBuiltSignal>(OnHexGridBuiltHandler);
        }

       
    }
}
