using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestElders.Models
{
    public class Herbivore : Animal
    {
        public Herbivore(Position position) : base(position) { }

        public override bool Eat() 
        {
            return false;
        }
    }
}
