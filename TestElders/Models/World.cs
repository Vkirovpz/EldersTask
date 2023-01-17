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
        private const int worldSize = 3;
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

            for (int i = 0; i < 4; i++)
            {
                var col = Dice.Roll(0, worldSize);
                var row = Dice.Roll(0, worldSize);
                var pos = Position.At(col, row);
                var type = Dice.Roll(0, 2);
                if (type == 0)
                    Cells.First(x => x.Position == pos).Visit(new Herbivore(pos));
                else
                    Cells.First(x => x.Position == pos).Visit(new Carnivore(pos));
            }

        }

        public void Cycle()
        {
            var animalsCopy = new List<Animal>();

            foreach (var animal in Animals)
            {
                animalsCopy.Add(animal);
            }

            for (int i = 0; i < animalsCopy.Count - 1; i++)
            {
                var animal = animalsCopy[i];
                var newPosition = animal.Move();
                if (IsValidPosition(newPosition))
                {
                    var oldCell = Cells.First(x => x.Position == animal.Position);
                    oldCell.Leave(animal);
                    var newCell = Cells.First(x => x.Position == newPosition);
                    newCell.Visit(animal);
                }
            }

            var carnivoresCopy = new List<Carnivore>();

            foreach (var carnivore in Carnivores)
            {
                carnivoresCopy.Add(carnivore);
            }   

            foreach (var carnivore in carnivoresCopy)
            {
                var cell = Cells.First(x => x.Position == carnivore.Position);
                var herbivoresInCell = cell.Animals.OfType<Herbivore>();
                if (herbivoresInCell.Count() > 0)
                {
                    var herbivore = herbivoresInCell.First();
                    if (carnivore.Eat())
                    {
                        cell.Leave(herbivore);
                    }
                }

            }

            //foreach (var animal in Animals)
            //{
            //    var newPosition = animal.Move();
            //    if (IsValidPosition(newPosition))
            //    {
            //        var oldCell = Cells.First(x => x.Position == animal.Position);
            //        oldCell.Leave(animal);
            //        var newCell = Cells.First(x => x.Position == newPosition);
            //        newCell.Visit(animal);
            //    }
            //}
        }

        private bool IsValidPosition(Position position)
        {
            return position.X > 0 && position.X < worldSize - 1 && position.Y > 0 && position.Y < worldSize - 1;
        }
    }
}
