using System.Collections.Generic;
using System.Linq;

namespace TBS.Grid {
    public class GameTile {
        private readonly GridPosition _gridPosition;
        private readonly List<Unit> units = new();
        
        public GameTile(GridPosition gridPosition) => _gridPosition = gridPosition;
        
        public override string ToString() {
            var unitString = units.Aggregate(string.Empty, (current, unit) => current + unit.name + "\n");

            return $"{_gridPosition} \n {unitString}";
        }

        public void AddUnit(Unit unit) => units.Add(unit);
        public void RemoveUnit(Unit unit) => units.Remove(unit);
        public List<Unit> GetUnitList() => units;
        public bool HasAnyUnit() => units.Count > 0;
        public Unit GetUnit() => HasAnyUnit() ? units[0] : null;
    }
}