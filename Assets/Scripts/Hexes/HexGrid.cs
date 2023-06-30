using Attributable;
using Attributable.Attributes;
using DG.Tweening;
using General.EventBus;
using General.Initialize;
using General.Signals;
using Managers;
using UnityEngine;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;
using Units;
using VContainer;

namespace Hexes
{
    public class HexGrid : MonoBehaviour, IInitializable
    {
        [SerializeField] private int _radius = 5;
        [SerializeField] private float _hexSize = 1f;
        [SerializeField] private HexCell _hexCellPrefab;
        [SerializeField] private Transform _parent;

        [ShowInInspector, ReadOnly] private HexCell _selectedHex;
        
        private HexCell[,] _hexCells;
        
        private EventBus _eventBus;
        private IObjectResolver _objectResolver;

        [Inject]
        private void Construct(EventBus eventBus, IObjectResolver objectResolver)
        {
            _eventBus = eventBus;
            _objectResolver = objectResolver;
        }
        
        private void OnHexSelectedHandler(HexSelectedSignal hexSelectedSignal)
        {
            if (_selectedHex != null && _selectedHex.ContainAttribute<DynamicsTag, SelectedAttribute>())
            {
                _selectedHex.RemoveAttribute<DynamicsTag, SelectedAttribute>();
            }
            
            _selectedHex = hexSelectedSignal.Hex;
        }
        
        public void Initialize()
        {
            GenerateGrid();
            _eventBus.Subscribe<HexSelectedSignal>(OnHexSelectedHandler);
        }
        
        private void OnDisable()
        {
            _eventBus.Unsubscribe<HexSelectedSignal>(OnHexSelectedHandler);
        }

        [Button]
        private void GenerateGrid()
        {
            ClearGrid();

            _hexCells = new HexCell[_radius * 2 + 1, _radius * 2 + 1];

            float xOffset = _hexSize * Mathf.Sqrt(3f);
            float yOffset = _hexSize * 1.5f;

            for (int q = -_radius; q <= _radius; q++)
            {
                int r1 = Mathf.Max(-_radius, -q - _radius);
                int r2 = Mathf.Min(_radius, -q + _radius);

                for (int r = r1; r <= r2; r++)
                {
                    float xPos = xOffset * q;
                    float yPos = yOffset * r + q * 0.5f * yOffset;

                    var hexCell = Instantiate(_hexCellPrefab, _parent, false);
                    hexCell.transform.localPosition = new Vector3(xPos, yPos, 0f);
                    hexCell.SetCoordinates(q, r);
                    hexCell.transform.SetParent(transform);
                    
                    _objectResolver.Inject(hexCell);
                    
                    hexCell.Initialize();

                    _hexCells[q + _radius, r + _radius] = hexCell;
                }
            }
        }

        private void OnHexagonClicked(HexCell hexCell)
        {
            if (_selectedHex != null)
            {
                if (_selectedHex != null && hexCell != null)
                {
                    if (_selectedHex.ContainAttribute<DynamicsTag, SelectedAttribute>())
                    {
                        _selectedHex.transform.DOMove(hexCell.transform.position, 1f);
                    }
                }
            }
            else
            {
                Debug.Log("Selected Hexagon: " + hexCell.Coordinates);
            }
        }
        
        private void ClearGrid()
        {
            if (_hexCells != null)
            {
                for (int q = 0; q < _hexCells.GetLength(0); q++)
                {
                    for (int r = 0; r < _hexCells.GetLength(1); r++)
                    {
                        if (_hexCells[q, r] != null)
                        {
                            DestroyImmediate(_hexCells[q, r].gameObject);
                            _hexCells[q, r] = null;
                        }
                    }
                }
            }
        }
    }
}