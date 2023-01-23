using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TestElders.Models
{
    public class Carnivore : Animal
    {

        public Carnivore(Cell cell, IRandomNumberGenerator rng) : base(cell, rng) { }

        public override void Eat()
        {
            var others = Cell.Animals.Where(x => x != this).OfType<Herbivore>();
            if (others.Any() == false)
                return;

            var herbivore = others.First();
            int randomValue = RandomNumberGenerator.GetBetween(0, 100);
            if (randomValue < AttackChance)
                herbivore.Die();
        }

        public override Animal Create(Cell cell) => new Carnivore(cell, RandomNumberGenerator);
    }
}