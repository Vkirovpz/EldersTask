using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TestElders.Models
{
    public class World
    {
        private const int worldSize = 5;
        private readonly List<Cell> Cells = new();

        public IEnumerable<Animal> Animals
        {
            get
            {
                return Cells.SelectMany(x => x.Animals);
            }
        }

        public IEnumerable<Herbivore> Herbivores
        {
            get
            {
                return Animals.OfType<Herbivore>();
            }
        }

        public IEnumerable<Carnivore> Carnivores
        {
            get
            {
                return Animals.OfType<Carnivore>();
            }
        }

        public World()
        {
            for (int x = 0; x < worldSize; x++)
            {
                for (int y = 0; y < worldSize; y++)
                {
                    Cells.Add(new Cell(new Position(x, y)));
                }
            }

            var col = Dice.Roll(0, worldSize + 1);
            var row = Dice.Roll(0, worldSize + 1);
            var pos = Position.At(col, row);
            var type = Dice.Roll(0, 2);
            if (type == 0)
                Cells.First(x => x.Position == pos).Visit(new Herbivore(pos));
            else
                Cells.First(x => x.Position == pos).Visit(new Carnivore(pos));
        }

        public void Cycle()
        {
            foreach (var animal in Animals)
            {
                var newPosition = animal.Move();
                if (IsValidPosition(newPosition))
                {
                    var oldCell = Cells.First(x => x.Position == animal.Position);
                    oldCell.Leave(animal);
                    var newCell = Cells.First(x => x.Position == newPosition);
                    newCell.Visit(animal);
                }
            }

            foreach (var animal in Animals)
            {
                animal.Eat();
            }
        }

        private bool IsValidPosition(Position position)
        {
            return position.X >= 0 && position.X <= worldSize && position.Y >= 0 && position.Y <= worldSize;
        }


        //public static void PrintMatrix(char[,] matrix)
        //{
        //    for (int row = 0; row < matrix.GetLength(0); row++)
        //    {
        //        for (int col = 0; col < matrix.GetLength(1); col++)
        //        {
        //            Console.Write("{0}", matrix[row, col]);
        //        }
        //        Console.WriteLine();
        //    }
        //}
    }
}
