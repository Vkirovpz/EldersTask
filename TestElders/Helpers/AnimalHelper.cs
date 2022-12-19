using TestElders.Models;

namespace TestElders.Helpers
{
    internal static class AnimalHelper
    {
        private static List<string> directions = new List<string> { "up", "down", "left", "right" };
        internal static List<Carnivore> Carnivores = new();
        internal static List<Herbivore> Herbivores = new();
        internal static (List<Carnivore>, List<Herbivore>) InitAnimals(int herbivoresCount, int carnivoresCount)
        {
            for (int i = 0; i < herbivoresCount; i++)
            {
                var herbivore = new Herbivore();
                Herbivores.Add((herbivore));
            }
            for (int i = 0; i < carnivoresCount; i++)
            {
                var carnivore = new Carnivore();
                Carnivores.Add((carnivore));
            }
            return (Carnivores, Herbivores);
        }
        internal static void Move(List<Herbivore> herbivores, List<Carnivore> carnivores, char[,] matrix, int huntChance)
        {
            foreach (var herbivore in herbivores.ToList())
            {
                bool repeat = true;
                while (repeat)
                {
                    int position = Utils.GetRandomNumber(0, directions.Count);
                    var direction = directions[position];

                    herbivore.Move(matrix, direction);

                    if (matrix[herbivore.Row, herbivore.Col] == 'x')
                    {
                        int randomValue = Utils.GetRandomNumber(0, 100);
                        if (randomValue < huntChance)
                        {
                            herbivores.Remove(herbivore);
                            matrix[herbivore.Row, herbivore.Col] = 'x';

                        }
                        repeat = false;
                    }
                    else
                    {
                        matrix[herbivore.Row, herbivore.Col] = herbivore.Char;
                        repeat = false;
                    }
                }
                if (herbivores.Count == 0)
                {
                    break;
                }
            }

            foreach (var carnivore in carnivores.ToList())
            {
                bool repeat = true;
                while (repeat)
                {
                    int position = Utils.GetRandomNumber(0, directions.Count);
                    var direction = directions[position];

                    carnivore.Move(matrix, direction);

                    if (matrix[carnivore.Row, carnivore.Col] == '▒')
                    {
                        matrix[carnivore.Row, carnivore.Col] = carnivore.Char;
                        repeat = false;
                    }

                    else if (matrix[carnivore.Row, carnivore.Col] == 'o')
                    {
                        int randomValue = Utils.GetRandomNumber(0, 100);
                        if (randomValue < huntChance)
                        {
                            Herbivore eatenHerbivore = herbivores.FirstOrDefault(h => h.Row == carnivore.Row && h.Col == carnivore.Col);
                            if (eatenHerbivore != null)
                            {
                                herbivores.Remove(eatenHerbivore);
                            }
                            matrix[carnivore.Row, carnivore.Col] = carnivore.Char;
                            repeat = false;
                        }
                    }
                }
                if (herbivores.Count == 0)
                {
                    break;
                }
            }
        }
    }
}
