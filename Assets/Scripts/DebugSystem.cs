using TBS.Grid.Debug;
using UnityEngine;

namespace TBS {
    public class DebugSystem : MonoBehaviour {
        [SerializeField] private GameObject debugMenu;

        private void Update() {
            if (Input.GetKeyUp(KeyCode.F12)) {
                debugMenu.SetActive(!debugMenu.activeSelf);
            }
        }

        public void TileInfo() {
            var gridDebugObjects = FindObjectsOfType<GridDebugObject>();
            foreach (var gridDebugObject in gridDebugObjects) {
                gridDebugObject.Toggle();
            }
        }
    }
}
