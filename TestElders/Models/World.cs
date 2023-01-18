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
        private readonly List<Cell> Cells = new();
        private readonly int size;

        public IEnumerable<Animal> Animals => Cells.SelectMany(x => x.Animals);

        public IEnumerable<Herbivore> Herbivores => Animals.OfType<Herbivore>();

        public IEnumerable<Carnivore> Carnivores => Animals.OfType<Carnivore>();

        public bool CanCycle => Herbivores.Any() && Carnivores.Any();

        public World(int size, int animalsCount)
        {
            if (size <= 0) throw new ArgumentOutOfRangeException(nameof(size), "World size must be a positive number");
            if (animalsCount <= 0) throw new ArgumentOutOfRangeException(nameof(animalsCount), "The amount of animals must be a positive number");

            this.size = size;

            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    Cells.Add(new Cell(new Position(x, y)));
                }
            }

            for (int i = 0; i < animalsCount; i++)
            {
                var col = Dice.Roll(0, size);
                var row = Dice.Roll(0, size);
                var pos = Position.At(col, row);
                var type = Dice.Roll(0, 2);
                var cell = Cells.First(x => x.Position == pos);
                if (type == 0)
                    cell.Visit(new Herbivore(cell));
                else
                    cell.Visit(new Carnivore(cell));
            }
        }

        public void Cycle()
        {
            foreach (var animal in Animals.ToList())
            {
                var newPosition = animal.Move();
                if (IsValidPosition(newPosition) == false)
                    continue;

                var oldCell = Cells.First(x => x.Position == animal.Cell.Position);
                oldCell.Leave(animal);
                var newCell = Cells.First(x => x.Position == newPosition);
                newCell.Visit(animal);
            }

            foreach (var animal in Animals.ToList())
            {
                var cell = Cells.First(x => x.Position == animal.Cell.Position);
                animal.Eat();
            }
        }

        private bool IsValidPosition(Position position)
        {
            return position.X >= 0 && position.X < size && position.Y >= 0 && position.Y < size;
        }
    }
}
