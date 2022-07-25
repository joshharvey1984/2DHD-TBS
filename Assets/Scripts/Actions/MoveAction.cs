using System;
using System.Collections.Generic;
using TBS.Grid;
using UnityEngine;

namespace TBS.Actions {
    public class MoveAction : BaseAction {
        public event EventHandler OnStartMoving;
        public event EventHandler OnStopMoving;
        
        [SerializeField] private int maxMoveDistance = 4;
        
        public override string GetActionName() => "Move";

        public override void TakeAction(GridPosition gridPosition, Action onActionComplete) {
            throw new NotImplementedException();
        }

        public override List<GridPosition> GetValidActionGridPositionList() {
            var validGridPositionList = new List<GridPosition>();
            var unitGridPosition = unit.GetGridPosition();

            for (var x = -maxMoveDistance; x <= maxMoveDistance; x++) {
                for (var z = -maxMoveDistance; z <= maxMoveDistance; z++) {
                    var offsetGridPosition = new GridPosition(x, z, unit.GetGridPosition().Height);
                    var testGridPosition = unitGridPosition + offsetGridPosition;

                    if (!MapGrid.Instance.IsValidGridPosition(testGridPosition)) continue;
                    if (unitGridPosition == testGridPosition) continue;
                    if (MapGrid.Instance.HasAnyUnitOnGridPosition(testGridPosition)) continue;
                    if (!Pathfinding.Instance.IsWalkableGridPosition(testGridPosition)) continue;
                    if (!Pathfinding.Instance.HasPath(unitGridPosition, testGridPosition)) continue;

                    var pathfindingDistanceMultiplier = 10;
                    if (Pathfinding.Instance.GetPathLength(unitGridPosition, testGridPosition) > maxMoveDistance * pathfindingDistanceMultiplier) {
                        continue;
                    }
                    
                    validGridPositionList.Add(testGridPosition);
                }
            }

            return validGridPositionList;
        }
    }
}