using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestElders.Models
{
    public class Carnivore : Animal
    {
        private const int AttackChance = 95;

        public Carnivore(Cell cell) : base(cell) { }

        public override void Eat()
        {
            var other = Cell.Animals.Where(x => x != this).OfType<Herbivore>();
            if (other.Any() == false)
                return;

            var herbivore = other.First();
            int randomValue = Dice.Roll(0, 100);
            if (randomValue < AttackChance)
            {
                herbivore.Die();
            }
        }
    }
}