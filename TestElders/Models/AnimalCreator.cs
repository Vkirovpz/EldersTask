using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TestElders.Models
{
    public class AnimalCreator : IAnimalCreator
    {
        private readonly IRandomNumberGenerator randomNumberGenerator;

        public AnimalCreator(IRandomNumberGenerator randomNumberGenerator)
        {
            this.randomNumberGenerator = randomNumberGenerator ?? throw new ArgumentNullException(nameof(randomNumberGenerator));
        }

        public Animal Create(Cell cell)
        {
            if (cell is null) throw new ArgumentNullException(nameof(cell));

            var type = randomNumberGenerator.GetBetween(0, 2);
            if (type == 0)
                return new Carnivore(cell, randomNumberGenerator);
            else if (type == 1)
                return new Herbivore(cell, randomNumberGenerator);

            throw new NotSupportedException($"Unsupported animal type {type}");
        }
    }
}
