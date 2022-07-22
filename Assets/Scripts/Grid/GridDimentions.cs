using System;

namespace TBS.Grid {
    [Serializable]
    public struct GridDimensions {
        public int x;
        public int z;
        public float cellSize;

        public int height;
        public float cellHeightSize;
    }
}
