using UnityEngine;

namespace TBS.Grid {
    [RequireComponent(typeof(MeshRenderer))]
    public class GridSystemVisualSingle : MonoBehaviour {

        [SerializeField] private MeshRenderer meshRenderer;
        
        public void Show(Material material) {
            meshRenderer.enabled = true;
            meshRenderer.material = material;
        }

        public void Hide() {
            meshRenderer.enabled = false;
        }
    }
}