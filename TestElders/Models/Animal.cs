using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TestElders.Models
{
    public abstract class Animal: IAnimalCreator
    {
        protected virtual int CouplingChance { get; } = 95;
        protected virtual int AttackChance { get; } = 95;

        protected Animal(Cell cell, IRandomNumberGenerator rng)
        {
            Cell = cell ?? throw new ArgumentNullException(nameof(cell));
            RandomNumberGenerator = rng ?? throw new ArgumentNullException(nameof(rng));
            Gender = Gender.Random(rng);
        }

        protected IRandomNumberGenerator RandomNumberGenerator { get; }

        public Cell Cell { get; private set; }
        public Gender Gender { get; }

        public void Move(Cell newCell)
        {
            Cell = newCell ?? throw new ArgumentNullException(nameof(newCell));
        }

        public Position Move()
        {
            var direction = Direction.Random(RandomNumberGenerator);
            return Cell.Position.To(direction);
        }

        public abstract Animal Create(Cell cell);

        public abstract void Eat();

        public void Die() => Cell.Leave(this);

        public virtual void Couple()
        {
            if (Gender == Gender.Male)
                return;

            var others = Cell.Animals.Where(x => x != this).Where(x => x.GetType() == GetType());
            if (others.Any() == false)
                return;

            var otherAnimals = others.FirstOrDefault(x => x.Gender != Gender);
            if (otherAnimals is null)
                return;

            int randomValue = RandomNumberGenerator.GetBetween(0, 100);
            if (randomValue < CouplingChance)
                Cell.Spawn(this);
        }
    }
}