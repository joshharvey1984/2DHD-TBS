using System;
using TBS.Actions;
using UnityEngine;

namespace TBS {
    public class UnitActionSystem : MonoBehaviour {
        public static UnitActionSystem Instance { get; private set; }

        private Camera mainCamera;
        
        public event EventHandler OnSelectedUnitChanged;
        public event EventHandler OnSelectedActionChanged;
        public event EventHandler<bool> OnBusyChanged;
        public event EventHandler OnActionStarted;


        [SerializeField] private Unit selectedUnit;
        [SerializeField] private LayerMask unitLayerMask;

        private BaseAction selectedAction;

        private void Awake() {
            if (Instance != null) {
                Debug.LogError($"There's more than one {name}! {transform} - {Instance}");
                Destroy(gameObject);
                return;
            }
            Instance = this;
            
            mainCamera = Camera.main;
        }

        private void Update() {
            if (TryHandleUnitSelection()) {
                return;
            }
        }
        
        private bool TryHandleUnitSelection() {
            if (!InputManager.Instance.IsMouseButtonDownThisFrame()) return false;
            
            var ray = mainCamera.ScreenPointToRay(InputManager.Instance.GetMouseScreenPosition());
            if (!Physics.Raycast(ray, out var raycastHit, float.MaxValue, unitLayerMask)) return false;
            
            if (!raycastHit.transform.TryGetComponent(out Unit unit)) return false;
            if (unit == selectedUnit) { return false; }
            if (unit.IsEnemy()) { return false; }

            SetSelectedUnit(unit);
            return true;
        }

        private void SetSelectedUnit(Unit unit) {
            selectedUnit = unit;
            SetSelectedAction(unit.GetAction<MoveAction>());
            
            OnSelectedUnitChanged?.Invoke(this, EventArgs.Empty);
        }

        public void SetSelectedAction(BaseAction baseAction) {
            selectedAction = baseAction;

            OnSelectedActionChanged?.Invoke(this, EventArgs.Empty);
        }

        public Unit GetSelectedUnit() => selectedUnit;
        public BaseAction GetSelectedAction() => selectedAction;
    }
}
