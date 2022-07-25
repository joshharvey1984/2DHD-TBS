using System.Collections;
using System.Collections.Generic;
using TBS.Grid;
using UnityEngine;

namespace TBS {
    public class Pathfinding : MonoBehaviour {
        public static Pathfinding Instance { get; private set; }
        
        private const int MOVE_STRAIGHT_COST = 10;
        private const int MOVE_DIAGONAL_COST = 14;

        private GridDimensions _gridDimensions;
        private GridSystem<PathNode> _gridSystem;

        private void Awake() {
            if (Instance != null) {
                Debug.LogError($"There's more than one {name}! {transform} - {Instance}");
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }

        public void Setup(GridDimensions gridDimensions) {
            _gridDimensions = gridDimensions;

            _gridSystem = new GridSystem<PathNode>(_gridDimensions, position => new PathNode(position));

            for (var x = 0; x < _gridDimensions.x; x++) {
                for (var z = 0; z < _gridDimensions.z; z++) {
                    for (var h = 0; h < _gridDimensions.height; h++) {
                        var gridPosition = new GridPosition(x, z, h);
                        GetNode(x, z, h).SetIsWalkable(true);
                    }
                }
            }
        }

        public List<GridPosition> FindPath(GridPosition startGridPosition, GridPosition endGridPosition,
            out int pathLength) {
            var openList = new List<PathNode>();
            var closedList = new List<PathNode>();

            var startNode = _gridSystem.GetGridObject(startGridPosition);
            var endNode = _gridSystem.GetGridObject(startGridPosition);
            openList.Add(startNode);
            
            for (var x = 0; x < _gridSystem.GetX(); x++) {
                for (var z = 0; z < _gridSystem.GetZ(); z++) {
                    for (var h = 0; h < _gridSystem.GetHeight(); h++) {
                        var gridPosition = new GridPosition(x, z, h);
                        var pathNode = _gridSystem.GetGridObject(gridPosition);

                        pathNode.SetGCost(int.MaxValue);
                        pathNode.SetHCost(0);
                        pathNode.CalculateFCost();
                        pathNode.ResetCameFromPathNode();
                    }
                }
            }
            
            startNode.SetGCost(0);
            startNode.SetHCost(CalculateDistance(startGridPosition, endGridPosition));
            startNode.CalculateFCost();

            while (openList.Count > 0) {
                var currentNode = GetLowestFCostPathNode(openList);

                if (currentNode == endNode) {
                    pathLength = endNode.GetFCost();
                    return CalculatePath(endNode);
                }

                openList.Remove(currentNode);
                closedList.Add(currentNode);
                
                foreach (var neighbourNode in GetNeighbourList(currentNode)) {
                    if (closedList.Contains(neighbourNode)) continue;
                    if (!neighbourNode.IsWalkable()) continue;

                    var tentativeGCost = currentNode.GetGCost() +
                                         CalculateDistance(currentNode.GetGridPosition(),
                                             neighbourNode.GetGridPosition());

                    if (tentativeGCost >= neighbourNode.GetGCost()) continue;
                    
                    neighbourNode.SetCameFromPathNode(currentNode);
                    neighbourNode.SetGCost(tentativeGCost);
                    neighbourNode.SetHCost(CalculateDistance(neighbourNode.GetGridPosition(), endGridPosition));
                    neighbourNode.CalculateFCost();
                        
                    if (!openList.Contains(neighbourNode)) openList.Add(neighbourNode);
                }
            }

            pathLength = 0;
            return null;
        }
        
        private List<GridPosition> CalculatePath(PathNode endNode) {
            
            var pathNodeList = new List<PathNode> { endNode };
            
            var currentNode = endNode;
            while (currentNode.GetCameFromPathNode() != null) {
                pathNodeList.Add(currentNode.GetCameFromPathNode());
                currentNode = currentNode.GetCameFromPathNode();
            }
            
            pathNodeList.Reverse();

            var gridPositionList = new List<GridPosition>();
            pathNodeList.ForEach(p => gridPositionList.Add(p.GetGridPosition()));

            return gridPositionList;
        }

        private PathNode GetLowestFCostPathNode(List<PathNode> pathNodeList) {
            var lowestFCostPathNode = pathNodeList[0];
            foreach (var t in pathNodeList) {
                if (t.GetFCost() < lowestFCostPathNode.GetFCost()) lowestFCostPathNode = t;
            }
            return lowestFCostPathNode;
        }

        private int CalculateDistance(GridPosition startGridPosition, GridPosition endGridPosition) {
            var gridPositionDistance = startGridPosition - endGridPosition;
            var xDistance = Mathf.Abs(gridPositionDistance.X);
            var zDistance = Mathf.Abs(gridPositionDistance.Z);
            var hDistance = Mathf.Abs(gridPositionDistance.Height);
            var remaining = Mathf.Abs(xDistance - zDistance - hDistance);
            return MOVE_DIAGONAL_COST * Mathf.Min(xDistance, zDistance) + MOVE_STRAIGHT_COST * remaining;
        }

        private PathNode GetNode(int x, int z, int h) => _gridSystem.GetGridObject(new GridPosition(x, z, h));
        
        private List<PathNode> GetNeighbourList(PathNode currentNode)
        {
            var neighbourList = new List<PathNode>();

            var gridPosition = currentNode.GetGridPosition();

            if (gridPosition.X - 1 >= 0)
            {
                // Left
                neighbourList.Add(GetNode(gridPosition.X - 1, gridPosition.Z + 0, gridPosition.Height));
                if (gridPosition.Z - 1 >= 0)
                {
                    // Left Down
                    neighbourList.Add(GetNode(gridPosition.X - 1, gridPosition.Z - 1, gridPosition.Height));
                }

                if (gridPosition.Z + 1 < _gridSystem.GetZ())
                {
                    // Left Up
                    neighbourList.Add(GetNode(gridPosition.X - 1, gridPosition.Z + 1, gridPosition.Height));
                }
            }

            if (gridPosition.X + 1 < _gridSystem.GetX())
            {
                // Right
                neighbourList.Add(GetNode(gridPosition.X + 1, gridPosition.Z + 0, gridPosition.Height));
                if (gridPosition.Z - 1 >= 0)
                {
                    // Right Down
                    neighbourList.Add(GetNode(gridPosition.X + 1, gridPosition.Z - 1, gridPosition.Height));
                }
                if (gridPosition.Z + 1 < _gridSystem.GetZ())
                {
                    // Right Up
                    neighbourList.Add(GetNode(gridPosition.X + 1, gridPosition.Z + 1, gridPosition.Height));
                }
            }

            if (gridPosition.Z - 1 >= 0)
            {
                // Down
                neighbourList.Add(GetNode(gridPosition.X + 0, gridPosition.Z - 1, gridPosition.Height));
            }
            if (gridPosition.Z + 1 < _gridSystem.GetZ())
            {
                // Up
                neighbourList.Add(GetNode(gridPosition.X + 0, gridPosition.Z + 1, gridPosition.Height));
            }

            return neighbourList;
        }

        public int GetPathLength(GridPosition startGridPosition, GridPosition endGridPosition) {
            FindPath(startGridPosition, endGridPosition, out var pathLength);
            return pathLength;
        }

        public bool IsWalkableGridPosition(GridPosition gridPosition) => 
            _gridSystem.GetGridObject(gridPosition).IsWalkable();

        public bool HasPath(GridPosition startGridPosition, GridPosition endGridPosition) => 
            FindPath(startGridPosition, endGridPosition, out _) != null;
    }
}
