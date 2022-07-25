using TBS.Grid;

namespace TBS {
    public class PathNode {
        private GridPosition _gridPosition;
        private int gCost;
        private int hCost;
        private int fCost;
        private PathNode cameFromPathNode;
        private bool isWalkable = true;

        public PathNode(GridPosition gridPosition) => _gridPosition = gridPosition;

        public override string ToString() => _gridPosition.ToString();
        
        public int GetGCost() => gCost;
        public int GetHCost() => hCost;
        public int GetFCost() => fCost;
        public void SetGCost(int gCost) => this.gCost = gCost;
        public void SetHCost(int hCost) => this.hCost = hCost;
        public void CalculateFCost() => fCost = gCost + hCost;
        public void ResetCameFromPathNode() => cameFromPathNode = null;
        public void SetCameFromPathNode(PathNode pathNode) => cameFromPathNode = pathNode;
        public PathNode GetCameFromPathNode() => cameFromPathNode;
        public GridPosition GetGridPosition() => _gridPosition;
        public bool IsWalkable() => isWalkable;
        public void SetIsWalkable(bool isWalkable) => this.isWalkable = isWalkable;
    }
}