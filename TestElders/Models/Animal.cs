using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TestElders.Models
{
    public abstract class Animal
    {
        public Animal(Cell cell, Gender gender)
        {
            Cell = cell ?? throw new ArgumentNullException(nameof(cell));
            Gender = gender ?? throw new ArgumentNullException(nameof(gender));
        }

        public Cell Cell { get; private set; }
        public Gender Gender { get; }

        public void Move(Cell newCell)
        {
            Cell = newCell ?? throw new ArgumentNullException(nameof(newCell));
        }

        public Position Move(IRandomNumberGenerator rng)
        {
            var direction = Direction.Random(rng);
            return Cell.Position.To(direction);
        }

        public void Die() => Cell.Leave(this);
        public abstract void Eat(IRandomNumberGenerator rng);
        public abstract void Coupling(IRandomNumberGenerator rng);
    }
}