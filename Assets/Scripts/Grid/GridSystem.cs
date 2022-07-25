using System;
using UnityEngine;

namespace TBS.Grid {
    public class GridSystem<TGridObject> {
        private GridDimensions _gridDimensions;

        private TGridObject[,,] _gridObjects;

        public GridSystem(GridDimensions gridDimensions, Func<GridPosition, TGridObject> createGridObject) {
            _gridDimensions = gridDimensions;

            _gridObjects = new TGridObject[gridDimensions.x, gridDimensions.z, gridDimensions.height];

            for (var x = 0; x < gridDimensions.x; x++) {
                for (var z = 0; z < gridDimensions.z; z++) {
                    for (var h = 0; h < gridDimensions.height; h++) {
                        var gridPosition = new GridPosition(x, z, h);
                        _gridObjects[x, z, h] = createGridObject(gridPosition);
                    }
                }
            }
        }
        
        public TGridObject GetGridObject(GridPosition gridPosition) => 
            _gridObjects[gridPosition.X, gridPosition.Z, gridPosition.Height];

        public GridPosition GetGridPosition(Vector3 worldPosition) {
            return new GridPosition(
                 Mathf.RoundToInt(worldPosition.x / _gridDimensions.cellSize),
                 Mathf.RoundToInt(worldPosition.z / _gridDimensions.cellSize), 
                 Mathf.RoundToInt(worldPosition.y / _gridDimensions.cellHeightSize));
        }

        public Vector3 GetWorldPosition(GridPosition gridPosition) {
            return new Vector3(
                gridPosition.X * _gridDimensions.cellSize,
                gridPosition.Height * _gridDimensions.cellHeightSize,
                gridPosition.Z * _gridDimensions.cellSize
            );
        }

        public bool IsValidGridPosition(GridPosition testGridPosition) {
            return testGridPosition.X >= 0 &&
                   testGridPosition.Z >= 0 &&
                   testGridPosition.Height >= 0 &&
                   testGridPosition.X < _gridDimensions.x &&
                   testGridPosition.Z < _gridDimensions.z &&
                   testGridPosition.Height < _gridDimensions.height;
        }

        public int GetX() => _gridDimensions.x;
        public int GetZ() => _gridDimensions.z;
        public int GetHeight() => _gridDimensions.height;

    }
}
