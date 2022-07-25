using System;
using TBS.Grid;
using UnityEngine;

namespace TBS {
    public class MapGrid : MonoBehaviour {
        public static MapGrid Instance { get; private set; }
        
        [SerializeField] private GridDimensions gridDimensions;
        private GridSystem<GameTile> _gridSystem;

        private void Awake() {
            if (Instance != null)
            {
                Debug.LogError("There's more than one MapGrid! " + transform + " - " + Instance);
                Destroy(gameObject);
                return;
            }
            Instance = this;

            _gridSystem = new GridSystem<GameTile>(gridDimensions, position => new GameTile(position));
        }

        private void Start() {
            Pathfinding.Instance.Setup(gridDimensions);
        }

        public int GetX() => gridDimensions.x;
        public int GetZ() => gridDimensions.z;
        public int GetHeight() => gridDimensions.height;

        public Vector3 GetWorldPosition(GridPosition gridPosition) => _gridSystem.GetWorldPosition(gridPosition);
        public GameTile GetGameTile(GridPosition gridPosition) => _gridSystem.GetGridObject(gridPosition);
        public GridPosition GetGridPosition(Vector3 worldPosition) => _gridSystem.GetGridPosition(worldPosition);

        public void AddUnitAtGridPosition(GridPosition gridPosition, Unit unit) => 
            _gridSystem.GetGridObject(gridPosition).AddUnit(unit);

        public bool IsValidGridPosition(GridPosition testGridPosition) => 
            _gridSystem.IsValidGridPosition(testGridPosition);

        public bool HasAnyUnitOnGridPosition(GridPosition testGridPosition) => 
            _gridSystem.GetGridObject(testGridPosition).HasAnyUnit();
    }
}
