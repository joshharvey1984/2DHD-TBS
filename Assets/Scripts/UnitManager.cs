using System;
using System.Collections.Generic;
using UnityEngine;

namespace TBS
{
    public class UnitManager : MonoBehaviour {
        public UnitManager Instance { get; private set; }
        
        private List<Unit> unitList = new();
        private List<Unit> friendlyUnitList = new();
        private List<Unit> enemyUnitList = new();

        private void Awake() {
            if (Instance != null) {
                Debug.LogError("There's more than one UnitManager! " + transform + " - " + Instance);
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }

        private void Start() {
            Unit.OnAnyUnitSpawned += Unit_OnAnyUnitSpawned;
        }

        private void Unit_OnAnyUnitSpawned(object sender, EventArgs e) {
            var unit = sender as Unit;
            unitList.Add(unit);
            
            if (unit != null && unit.IsEnemy()) {
                enemyUnitList.Add(unit);
            } else {
                friendlyUnitList.Add(unit);
            }
        }

        private void Unit_OnAnyUnitDead(object sender, EventArgs e) {
            var unit = sender as Unit;
            unitList.Remove(unit);
            
            if (unit != null && unit.IsEnemy()) {
                enemyUnitList.Remove(unit);
            } else {
                friendlyUnitList.Remove(unit);
            }
        }

        public List<Unit> GetUnitList() => unitList;
        public List<Unit> GetFriendlyUnitList() => friendlyUnitList;
        public List<Unit> GetEnemyUnitList() => enemyUnitList;
    }
}
