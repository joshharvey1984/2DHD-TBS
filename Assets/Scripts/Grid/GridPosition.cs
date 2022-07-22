using System;

namespace TBS.Grid {
    public struct GridPosition {
        public int X;
        public int Z;
        public int Height;

        public GridPosition(int x, int z, int height) {
            X = x;
            Z = z;
            Height = height;
        }
        
        public override bool Equals(object obj) =>
            obj is GridPosition position &&
            Z == position.Z &&
            X == position.X &&
            Height == position.Height;

        public bool Equals(GridPosition other) => this == other;
        public override int GetHashCode() => HashCode.Combine(X, Z, Height);
        public override string ToString() => $"x: {X}; z: {Z}; Height: {Height}";

        public static bool operator ==(GridPosition a, GridPosition b) {
            return a.X == b.X && a.Z == b.Z;
        }

        public static bool operator !=(GridPosition a, GridPosition b) {
            return !(a == b);
        }

        public static GridPosition operator +(GridPosition a, GridPosition b) {
            return new GridPosition(a.X + b.X, a.Z + b.Z, a.Height + b.Height);
        }

        public static GridPosition operator -(GridPosition a, GridPosition b) {
            return new GridPosition(a.X - b.X, a.Z - b.Z, a.Height + b.Height);
        }

    }
}
