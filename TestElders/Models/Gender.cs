using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestElders.Models
{
    public class Gender : IEquatable<Gender>
    {
        public readonly static Gender Male = new Gender("male");
        public readonly static Gender Female = new Gender("female");

        private readonly static IList<string> possibleGenders = new List<string>() { "male", "female" };
        private string value;

        Gender(string value)
        {
            this.value = value;
        }

        public static Gender Random(IRandomNumberGenerator rng)
        {
            var genderIndex = rng.GetBetween(0, 2);
            var value= possibleGenders.ElementAtOrDefault(genderIndex);
            if (string.IsNullOrEmpty(value))
                throw new NotSupportedException($"Unsupported gender {genderIndex}");

            var gender = new Gender(value);
            return gender;
        }

        public override string ToString() => value;

        public override bool Equals(object obj) => Equals(obj as Gender);

        public override int GetHashCode() => value.GetHashCode();

        public bool Equals(Gender other)
        {
            if (other is null) return false;

            return other.value == value;
        }

        public static bool operator ==(Gender obj1, Gender obj2) => obj1.Equals(obj2);

        public static bool operator !=(Gender obj1, Gender obj2) => obj1 == obj2 == false;
    }
}
