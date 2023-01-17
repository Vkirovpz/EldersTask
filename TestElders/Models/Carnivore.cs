using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestElders.Models
{
    public class Carnivore : Animal
    {
        private const int attackChance = 60;

        public Carnivore(Position position) : base(position) { }

        public override bool Eat()
        {
            int randomValue = Random.Shared.Next(0, 100);
            if (randomValue < attackChance)
            {
                return true;
            }
            else { return false; }
        }
    }
}