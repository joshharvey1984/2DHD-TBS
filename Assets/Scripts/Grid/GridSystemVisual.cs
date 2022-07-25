using System;
using System.Collections.Generic;
using TBS.Grid.Debug;
using UnityEngine;

namespace TBS.Grid {
    public class GridSystemVisual : MonoBehaviour {
        
        [Serializable]
        public struct GridVisualTypeMaterial {
            public GridVisualType gridVisualType;
            public Material material;
        }

        public enum GridVisualType {
            White,
            Blue,
            Red,
            RedSoft,
            Yellow,
        }

        [SerializeField] private GameObject gridVisualPrefab;
        [SerializeField] private List<GridVisualTypeMaterial> gridVisualTypeMaterialList;
        
        private GridSystemVisualSingle[,,] _gridSystemVisualSingleArray;

        private void Start() {
            _gridSystemVisualSingleArray = new GridSystemVisualSingle[
                MapGrid.Instance.GetX(), 
                MapGrid.Instance.GetZ(), 
                MapGrid.Instance.GetHeight()
            ];
            
            CreateGridVisuals();
            
            UnitActionSystem.Instance.OnSelectedActionChanged += UnitActionSystem_OnSelectedActionChanged;
            
            UpdateGridVisuals();
        }
        
        private void CreateGridVisuals() {
            for (var x = 0; x < MapGrid.Instance.GetX(); x++) {
                for (var z = 0; z < MapGrid.Instance.GetZ(); z++) {
                    for (var h = 0; h < MapGrid.Instance.GetHeight(); h++) {
                        
                        var gridPosition = new GridPosition(x, z, h);
                        var gridVisual = Instantiate(
                            gridVisualPrefab, 
                            MapGrid.Instance.GetWorldPosition(gridPosition), 
                            gridVisualPrefab.transform.rotation);
                        
                        gridVisual.GetComponent<GridDebugObject>()
                            .SetGridObject(MapGrid.Instance.GetGameTile(gridPosition));

                        gridVisual.transform.parent = gameObject.transform;
                        
                        _gridSystemVisualSingleArray[x, z, h] = gridVisual.GetComponent<GridSystemVisualSingle>();
                    }
                }
            }
        }

        private void UpdateGridVisuals() {
            HideAllGridPosition();

            var selectedUnit = UnitActionSystem.Instance.GetSelectedUnit();
            if (selectedUnit is null) return;
            
            var selectedAction = UnitActionSystem.Instance.GetSelectedAction();
            
            ShowGridPositionList(selectedAction.GetValidActionGridPositionList(), selectedAction.GetGridVisualType());
        }

        private void ShowGridPositionList(List<GridPosition> gridPositionList, GridVisualType gridVisualType) {
            foreach (var gridPosition in gridPositionList) {
                _gridSystemVisualSingleArray[gridPosition.X, gridPosition.Z, gridPosition.Height].
                    Show(GetGridVisualTypeMaterial(gridVisualType));
            }

        }

        private void HideAllGridPosition() {
            foreach (var gridSystemVisualSingle in _gridSystemVisualSingleArray) { gridSystemVisualSingle.Hide(); }
        }
        
        private void UnitActionSystem_OnSelectedActionChanged(object sender, EventArgs e) {
            UpdateGridVisuals();
        }
        
        private Material GetGridVisualTypeMaterial(GridVisualType gridVisualType)
        {
            foreach (var gridVisualTypeMaterial in gridVisualTypeMaterialList) {
                if (gridVisualTypeMaterial.gridVisualType == gridVisualType) return gridVisualTypeMaterial.material;
            }

            UnityEngine.Debug.LogError("Could not find GridVisualTypeMaterial for GridVisualType " + gridVisualType);
            return null;
        }
    }
}
