namespace TestElders.Models
{
    public readonly struct Direction : IEquatable<Direction>
    {
        private readonly static Dictionary<int, string> directionsMap = new()
        {
            {0, "up" },
            {1, "down" },
            {2, "left" },
            {3, "right" }
        };

        public readonly static Direction Up = new(directionsMap[0]);
        public readonly static Direction Down = new(directionsMap[1]);
        public readonly static Direction Left = new(directionsMap[2]);
        public readonly static Direction Right = new(directionsMap[3]);

        private readonly string value;

        Direction(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException($"'{nameof(value)}' cannot be null or whitespace.", nameof(value));

            this.value = value;
        }

        public static Direction Random()
        {
            var direction = System.Random.Shared.Next(0, directionsMap.Count);
            return new Direction(directionsMap[direction]);
        }

        public override bool Equals(object obj) => Equals((Direction)obj);

        public override int GetHashCode() => value.GetHashCode();

        public bool Equals(Direction other)
        {
            return other.value.Equals(value);
        }

        public static bool operator ==(Direction obj1, Direction obj2)
        {
            return obj1.Equals(obj2);
        }

        public static bool operator !=(Direction obj1, Direction obj2)
        {
            return obj1 == obj2 == false;
        }
    }
}
