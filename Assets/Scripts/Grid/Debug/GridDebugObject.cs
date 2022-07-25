using TMPro;
using UnityEngine;

namespace TBS.Grid.Debug
{
    public class GridDebugObject : MonoBehaviour {
        [SerializeField] private TextMeshPro textMeshPro;

        private object _gridObject;
        
        protected void Update() {
            var gridText = _gridObject.ToString();
            textMeshPro.text = gridText;
        }
        
        public virtual void SetGridObject(object gridObject) => _gridObject = gridObject;
        public void Toggle() => textMeshPro.gameObject.SetActive(!textMeshPro.IsActive());
    }
}
