using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TestElders.Models
{
    public class World<TCarnivore, THerbivore>
        where TCarnivore : Animal
        where THerbivore : Animal
    {
        private readonly List<Cell> cells = new();
        private readonly int size;
        private readonly IRandomNumberGenerator rng;

        public IEnumerable<Animal> Animals => cells.SelectMany(x => x.Animals);

        public IEnumerable<THerbivore> Herbivores => Animals.OfType<THerbivore>();

        public IEnumerable<TCarnivore> Carnivores => Animals.OfType<TCarnivore>();

        public IEnumerable<Cell> Cells => cells.AsReadOnly();

        public bool CanCycle => Herbivores.Any() && Carnivores.Any();
            
        public World(int size, int animalsCount, IRandomNumberGenerator rng)
        {
            if (size <= 0) throw new ArgumentOutOfRangeException(nameof(size), "World size must be a positive number");
            if (animalsCount <= 0) throw new ArgumentOutOfRangeException(nameof(animalsCount), "The amount of animals must be a positive number");
            if (rng is null) throw new ArgumentNullException(nameof(rng));

            this.size = size;
            this.rng = rng;

            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    cells.Add(new Cell(new Position(x, y)));
                }
            }

            for (int i = 0; i < animalsCount; i++)
            {
                var col = rng.GetBetween(0, size);
                var row = rng.GetBetween(0, size);
                var pos = Position.At(col, row);
                var raceType = rng.GetBetween(0, 2);
                var gender = Gender.Random(rng);
                var cell = cells.First(x => x.Position == pos);
                if (raceType == 0)
                    cell.Spawn(new Herbivore(cell, gender));
                else
                    cell.Spawn(new Carnivore(cell, gender));
            }
        }

        public void Cycle()
        {
            foreach (var animal in Animals.ToList())
            {
                var cell = cells.First(x => x.Position == animal.Cell.Position);
                animal.Coupling(rng);
                animal.Eat(rng);
            }

            foreach (var animal in Animals.ToList())
            {
                var newPosition = animal.Move(rng);
                if (IsValidPosition(newPosition) == false)
                    continue;

                var oldCell = cells.First(x => x.Position == animal.Cell.Position);
                oldCell.Leave(animal);
                var newCell = cells.First(x => x.Position == newPosition);
                newCell.Visit(animal);
            }
        }

        private bool IsValidPosition(Position position)
        {
            return position.X >= 0 && position.X < size && position.Y >= 0 && position.Y < size;
        }
    }
}
