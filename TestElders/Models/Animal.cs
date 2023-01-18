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
        public Animal(Cell cell)
        {
            Cell = cell ?? throw new ArgumentNullException(nameof(cell));
        }

        public Cell Cell { get; private set; }

        public void Move(Cell newCell)
        {
            Cell = newCell ?? throw new ArgumentNullException(nameof(newCell));
        }

        public Position Move()
        {
            var direction = Direction.Random();
            return Cell.Position.To(direction);
        }

        public void Die()
        {
            Cell.Leave(this);
        }

        public abstract void Eat();
    }
}
