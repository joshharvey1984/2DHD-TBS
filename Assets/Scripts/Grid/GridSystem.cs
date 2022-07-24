using System;

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
    }
}
