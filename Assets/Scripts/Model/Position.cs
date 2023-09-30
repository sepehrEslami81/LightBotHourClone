using System;
using JetBrains.Annotations;

namespace Model
{
    /// <summary>
    /// A model to store the coordinates of the tiles
    /// </summary>
    [Serializable]
    public class Position
    {
        public int x;
        public int y;

        public Position (int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public static Position operator + ([NotNull] Position a, [NotNull] Position b) 
            => new Position(a.x + b.x, a.y + b.y);

        public static Position operator - ([NotNull] Position a, [NotNull] Position b) 
            => new Position(a.x - b.x, a.y - b.y);

        public static bool operator == ([NotNull] Position a, [NotNull] Position b)
            => a.x == b.x && a.y == b.y;

        public static bool operator != ([NotNull] Position a, [NotNull] Position b)
            => a.x != b.x || a.y != b.y;
    }
}