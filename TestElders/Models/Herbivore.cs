using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestElders.Models
{
    public class Herbivore : Animal
    {
        private const int CouplingChanceLimit = 5;
        public Herbivore(Cell cell, Gender gender) : base(cell, gender) { }

        public override void Eat(IRandomNumberGenerator rng) { }

        public override void Coupling(IRandomNumberGenerator rng)
        {
            var others = Cell.Animals.Where(x => x != this).OfType<Herbivore>();
            if (others.Any() == false)
                return;

            var otherHerbivore = others.FirstOrDefault(x => x.Gender != Gender);
            if (otherHerbivore == null)
                return;

            int randomValue = rng.GetBetween(0, 100);
            if (randomValue > CouplingChanceLimit)
                Cell.Spawn(new Herbivore(Cell, Gender.Random(rng)));
        }
    }
}
