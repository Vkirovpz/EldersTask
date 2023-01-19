namespace TestElders.Models
{
    public class Dice : IRandomNumberGenerator
    {
        public int GetBetween(int min, int max) => Random.Shared.Next(min, max);
    }
}
