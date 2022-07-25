using System;
using TBS.Actions;
using TBS.Grid;
using UnityEngine;

namespace TBS {
    public class Unit : MonoBehaviour {
        
        public static event EventHandler OnAnyActionPointsChanged;
        public static event EventHandler OnAnyUnitSpawned;
        public static event EventHandler OnAnyUnitDead;
        
        private bool isEnemy;
        private GridPosition gridPosition;
        private BaseAction[] baseActionArray;

        private void Awake() {
            baseActionArray = GetComponents<BaseAction>();
        }

        

        private void Start() {
            gridPosition = MapGrid.Instance.GetGridPosition(transform.position);
            MapGrid.Instance.AddUnitAtGridPosition(gridPosition, this);
            
            OnAnyUnitSpawned?.Invoke(this, EventArgs.Empty);
        }

        public T GetAction<T>() where T : BaseAction {
            foreach (var baseAction in baseActionArray) {
                if (baseAction is T action) return action;
            }

            return null;
        }
        
        public bool IsEnemy() => isEnemy;
        public GridPosition GetGridPosition() => gridPosition;
        public Vector3 GetWorldPosition() => transform.position;
        public BaseAction[] GetBaseActionArray() => baseActionArray;
    }
}
