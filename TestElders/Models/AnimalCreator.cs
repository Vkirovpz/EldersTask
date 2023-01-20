using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestElders.Models
{
    internal class AnimalCreator : IAnimalCreator
    {
        public Animal GetAnimal(string type, Cell cell, Gender gender)
        {
            if (type.ToLower().Equals("carnivore"))
                return new Carnivore(cell, gender);
            else if (type.ToLower().Equals("herbivore"))
                return new Herbivore(cell, gender);

            throw new NotSupportedException($"Unsupported animal type {type}");
        }
    }
}
