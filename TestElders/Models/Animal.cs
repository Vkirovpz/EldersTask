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
        public Animal(Position position)
        {
            Position = position;
        }

        public Position Position { get; private set; }

        public void Move(Position newPosition)
        {
            Position = newPosition;
        }

        public Position Move()
        {
            var direction = Random.Shared.Next(0, 4);
            if (direction == 0) return Position.Up();
            else if (direction == 1) return Position.Down();
            else if (direction == 2) return Position.Left();
            
            return Position.Right();
        }

        public abstract bool Eat();
    }
}
