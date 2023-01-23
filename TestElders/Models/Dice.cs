namespace TestElders.Models
{
    public class Dice : IRandomNumberGenerator
    {
        private static Dice shared;
        private readonly static object mutex = new object();

        public int GetBetween(int min, int max) => Random.Shared.Next(min, max);

        public static Dice Shared
        {
            get
            {
                if (shared is null)
                {
                    lock (mutex)
                    {
                        if (shared is null)
                        {
                            shared = new Dice();
                        }
                    }
                }

                return shared;
            }
        }
    }
}
