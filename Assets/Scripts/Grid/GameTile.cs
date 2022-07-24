namespace TBS.Grid {
    public class GameTile {
        private GridPosition _gridPosition;
        
        public GameTile(GridPosition gridPosition) {
            _gridPosition = gridPosition;
        }

        public override string ToString() {
            return _gridPosition.ToString();
        }
    }
}