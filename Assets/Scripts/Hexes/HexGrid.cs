using General.EventBus;
using General.Initialize;
using UnityEngine;
using Sirenix.OdinInspector;
using VContainer;

namespace Hexes
{
    public class HexGrid : MonoBehaviour, IInitializable
    {
        [SerializeField] private bool _circleGrid;
        [SerializeField, ShowIf("@_circleGrid")] private int _radius = 5;
        [SerializeField, ShowIf("@_circleGrid")] private float _hexSize = 1f;

        [SerializeField, HideIf("@_circleGrid")] private int _width = 5;
        [SerializeField, HideIf("@_circleGrid")] private int _height = 5;

        [SerializeField, HideIf("@_circleGrid")] private float _xOffset = 1f;
        [SerializeField, HideIf("@_circleGrid")] private float _yOffset = 1f;

        [SerializeField] private HexCell _hexCellPrefab;
        [SerializeField] private Transform _parent;

        private HexCell[][] _hexCells; // Modified line

        private EventBus _eventBus;
        private IObjectResolver _objectResolver;

        public HexCell[][] HexCells => _hexCells; // Modified line

        [Inject]
        private void Construct(EventBus eventBus, IObjectResolver objectResolver)
        {
            _eventBus = eventBus;
            _objectResolver = objectResolver;
        }

        public void Initialize()
        {
            GenerateGrid();
        }

        [Button]
        private void GenerateGrid()
        {
            if (_circleGrid) GenerateCircleGrid();
            else GenerateSquareGrid();
        }

        private void GenerateSquareGrid()
        {
            float gridWidth = _width * _xOffset;
            float gridHeight = _height * _yOffset;
            float startX = -gridWidth / 2f + _xOffset / 2f;
            float startY = gridHeight / 2f - _yOffset / 2f;

            _hexCells = new HexCell[_width][]; // Modified line

            for (int x = 0; x < _width; x++)
            {
                _hexCells[x] = new HexCell[_height]; // Modified line

                for (int y = 0; y < _height; y++)
                {
                    float xPos = startX + _xOffset * x;
                    float yPos = startY - _yOffset * y;

                    if (x % 2 == 1) yPos -= _yOffset * 0.5f;

                    var hexCell = Instantiate(_hexCellPrefab, _parent, false);
                    hexCell.transform.localPosition = new Vector3(xPos, yPos, 0);
                    hexCell.SetCoordinates(x, y);
                    hexCell.transform.SetParent(transform);

                    _objectResolver.Inject(hexCell);

                    hexCell.Initialize();

                    _hexCells[x][y] = hexCell; // Modified line
                }
            }

            _eventBus.Invoke(new HexGridBuiltSignal(this));
        }

        private void GenerateCircleGrid()
        {
            ClearGrid();

            _hexCells = new HexCell[_radius * 2 + 1][]; // Modified line

            float xOffset = _hexSize * Mathf.Sqrt(3f);
            float yOffset = _hexSize * 1.5f;

            for (int q = -_radius; q <= _radius; q++)
            {
                int r1 = Mathf.Max(-_radius, -q - _radius);
                int r2 = Mathf.Min(_radius, -q + _radius);

                _hexCells[q + _radius] = new HexCell[r2 - r1 + 1]; // Modified line

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

                    _hexCells[q + _radius][r - r1] = hexCell; // Modified line
                }
            }
        }

        private void ClearGrid()
        {
            if (_hexCells != null)
            {
                for (int q = 0; q < _hexCells.Length; q++) // Modified line
                {
                    if (_hexCells[q] != null)
                    {
                        for (int r = 0; r < _hexCells[q].Length; r++) // Modified line
                        {
                            if (_hexCells[q][r] != null) // Modified line
                            {
                                DestroyImmediate(_hexCells[q][r].gameObject); // Modified line
                                _hexCells[q][r] = null; // Modified line
                            }
                        }
                    }
                }
            }
        }
    }
}
