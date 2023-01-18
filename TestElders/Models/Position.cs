
namespace TestElders.Models
{
    public class Position : IEquatable<Position>
    {
        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; }
        public int Y { get; }

        public static Position At(int x, int y) => new (x, y);

        public Position Up() => new (X, Y - 1);
        public Position Down() => new (X, Y + 1);
        public Position Left() => new (X - 1, Y);
        public Position Right() => new (X + 1, Y);

        public Position To(Direction direction)
        {
            if (direction == Direction.Up) return Up();
            else if (direction == Direction.Down) return Down();
            else if (direction == Direction.Left) return Left();
            else if (direction == Direction.Right) return Right();

            throw new NotSupportedException($"Unsupported direction {direction}");
        }

        public override bool Equals(object obj) => Equals(obj as Position);

        public override int GetHashCode() => HashCode.Combine(X, Y);

        public bool Equals(Position other)
        {
            if (other is null) return false;

            return other.X == X && other.Y == Y;
        }

        public static bool operator ==(Position obj1, Position obj2)
        {
            return obj1.Equals(obj2);
        }

        public static bool operator !=(Position obj1, Position obj2)
        {
            return obj1 == obj2 == false;
        }
    }
}
