using System;
using System.Collections.Generic;
using TBS.Grid;
using UnityEngine;
using static TBS.Grid.GridSystemVisual;

namespace TBS.Actions
{
    public abstract class BaseAction : MonoBehaviour {
        public static event EventHandler OnAnyActionStarted;
        public static event EventHandler OnAnyActionCompleted;
        
        protected Unit unit;
        protected bool isActive;
        protected Action onActionComplete;

        protected GridVisualType _gridVisualType = GridVisualType.White;

        protected virtual void Awake() {
            unit = GetComponent<Unit>();
        }
        
        public abstract string GetActionName();
        public abstract void TakeAction(GridPosition gridPosition, Action onActionComplete);
        
        public virtual bool IsValidActionGridPosition(GridPosition gridPosition) {
            var validGridPositionList = GetValidActionGridPositionList();
            return validGridPositionList.Contains(gridPosition);
        }
    
        public abstract List<GridPosition> GetValidActionGridPositionList();
        
        protected void ActionStart(Action onActionComplete) {
            isActive = true;
            this.onActionComplete = onActionComplete;

            OnAnyActionStarted?.Invoke(this, EventArgs.Empty);
        }
        
        protected void ActionComplete() {
            isActive = false;
            onActionComplete();

            OnAnyActionCompleted?.Invoke(this, EventArgs.Empty);
        }

        public Unit GetUnit() => unit;
        public GridVisualType GetGridVisualType() => _gridVisualType;
    }
}
