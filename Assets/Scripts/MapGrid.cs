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

        public int GetX() => gridDimensions.x;
        public int GetZ() => gridDimensions.z;
        public int GetHeight() => gridDimensions.height;

        public Vector3 GetWorldPosition(GridPosition gridPosition) {
            return new Vector3(
                gridPosition.X * gridDimensions.cellSize,
                gridPosition.Height * gridDimensions.cellHeightSize,
                gridPosition.Z * gridDimensions.cellSize
            );
        }
    }
}
