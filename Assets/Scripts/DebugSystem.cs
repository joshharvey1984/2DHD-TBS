using TBS.Grid.Debug;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TBS {
    public class DebugSystem : MonoBehaviour {
        
        [SerializeField] private GameObject debugMenu;

        private void Awake() {
        }

        private void Update() {
            if (Keyboard.current.f12Key.wasPressedThisFrame) {
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
