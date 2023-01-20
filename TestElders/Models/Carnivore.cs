using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestElders.Models
{
    public class Carnivore : Animal
    {
        private const int AttackChanceLimit = 5;
        private const int CouplingChanceLimit = 5;

        public Carnivore(Cell cell, Gender gender) : base(cell, gender) { }

        public override void Coupling(IRandomNumberGenerator rng)
        {
            var others = Cell.Animals.Where(x => x != this).OfType<Carnivore>();
            if (others.Any() == false)
                return;

            var otherCarnivore = others.FirstOrDefault(x => x.Gender != Gender);
            if (otherCarnivore is null)
                return;

            int chance = rng.GetBetween(0, 100);
            if (chance > CouplingChanceLimit)
                Cell.Spawn(new Carnivore(Cell, Gender.Random(rng)));
        }

        public override void Eat(IRandomNumberGenerator rng)
        {
            var others = Cell.Animals.Where(x => x != this).OfType<Herbivore>();
            if (others.Any() == false)
                return;

            var herbivore = others.First();
            int chance = rng.GetBetween(0, 100);
            if (chance > AttackChanceLimit)
                herbivore.Die();
        }
    }
}