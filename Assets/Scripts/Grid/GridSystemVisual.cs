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

        [SerializeField] private GameObject gridSystemVisualSinglePrefab;
        [SerializeField] private List<GridVisualTypeMaterial> gridVisualTypeMaterialList;
        
        private GridSystemVisualSingle[,,] _gridSystemVisualSingleArray;

        private void Start() {
            _gridSystemVisualSingleArray = new GridSystemVisualSingle[
                MapGrid.Instance.GetX(), 
                MapGrid.Instance.GetZ(), 
                MapGrid.Instance.GetHeight()
            ];
            
            CreateGridVisuals();
            UpdateGridVisuals();
        }

        private void CreateGridVisuals() {
            for (var x = 0; x < MapGrid.Instance.GetX(); x++) {
                for (var z = 0; z < MapGrid.Instance.GetZ(); z++) {
                    for (var h = 0; h < MapGrid.Instance.GetHeight(); h++) {
                        
                        var gridPosition = new GridPosition(x, z, h);
                        var gridSystemVisualSingle = Instantiate(
                            gridSystemVisualSinglePrefab, 
                            MapGrid.Instance.GetWorldPosition(gridPosition), 
                            gridSystemVisualSinglePrefab.transform.rotation);
                        
                        gridSystemVisualSingle.GetComponent<GridDebugObject>()
                            .SetGridObject(MapGrid.Instance.GetGameTile(gridPosition));

                        gridSystemVisualSingle.transform.parent = gameObject.transform;
                        
                        _gridSystemVisualSingleArray[x, z, h] = 
                            gridSystemVisualSingle.GetComponent<GridSystemVisualSingle>();
                    }
                }
            }
        }

        private void UpdateGridVisuals() {
            HideAllGridPosition();
        }

        private void HideAllGridPosition() {
            foreach (var gridSystemVisualSingle in _gridSystemVisualSingleArray) { gridSystemVisualSingle.Hide(); }
        }

    }
}
