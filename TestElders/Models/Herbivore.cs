using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestElders.Models
{
    public class Herbivore : Animal
    {
        public Herbivore(Cell cell, IRandomNumberGenerator rng) : base(cell, rng) { }

        public override void Eat() { }

        public override Animal Create(Cell cell) => new Herbivore(cell, RandomNumberGenerator);
    }
}
