using UnityEngine;
using UnityEngine.InputSystem;

namespace TBS {
    public class InputManager : MonoBehaviour {
        public static InputManager Instance { get; private set; }

        private PlayerInputActions _playerInputActions;

        private void Awake() {
            if (Instance != null) {
                Debug.LogError("There's more than one Input Manager! " + transform + " - " + Instance);
                Destroy(gameObject);
                return;
            }
            Instance = this;
            
            _playerInputActions = new();
            _playerInputActions.Player.Enable();

        }

        public Vector2 GetMouseScreenPosition() => Mouse.current.position.ReadValue();
        public bool IsMouseButtonDownThisFrame() => _playerInputActions.Player.LeftClick.WasPressedThisFrame();
    }
}
