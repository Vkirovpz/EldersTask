namespace TestElders.Models
{
    public class Dice
    {
        public static int Roll(int min, int max) => Random.Shared.Next(min, max);
    }
}
