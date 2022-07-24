using TMPro;
using UnityEngine;

namespace TBS.Grid.Debug
{
    public class GridDebugObject : MonoBehaviour {
        [SerializeField] private TextMeshPro textMeshPro;

        private object _gridObject;
        
        protected void Update() {
            textMeshPro.text = _gridObject.ToString();
        }
        
        public virtual void SetGridObject(object gridObject) => _gridObject = gridObject;
        public void Toggle() => textMeshPro.gameObject.SetActive(!textMeshPro.IsActive());
    }
}
