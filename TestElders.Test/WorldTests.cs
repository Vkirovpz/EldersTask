using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TestElders.Models;

namespace TestElders.Test
{
    public class WorldTests
    {
        public class HerbivoreMock : Animal
        {
            public bool CouplingCalled { get; set; }
            public bool EatCalled { get; set; }

            public HerbivoreMock(Cell cell, IRandomNumberGenerator randomNumberGenerator) : base(cell, randomNumberGenerator) { }

            // public HerbivoreMock(Cell cell, Gender gender) : base(cell, gender) { }

            public override void Couple()
            {
                CouplingCalled = true;
            }

            public override void Eat()
            {
                EatCalled = true;
            }

            public override Animal Create(Cell cell)
            {
                return new HerbivoreMock(cell, RandomNumberGenerator);
            }
        }

        public class CarnivoreMock : Animal
        {
      
            public bool CouplingCalled { get; set; }
            public bool EatCalled { get; set; }

            public CarnivoreMock(Cell cell, IRandomNumberGenerator randomNumberGenerator) : base(cell, randomNumberGenerator) { }

            //public CarnivoreMock(Cell cell, Gender gender) : base(cell, gender) { }

            public override void Couple()
            {
                CouplingCalled = true;
            }

            public override void Eat()
            {
                var others = Cell.Animals.Where(x => x != this).OfType<HerbivoreMock>();
                if (others.Any() == false)
                    return;

                var herbivore = others.First();
                int randomValue = RandomNumberGenerator.GetBetween(0, 1);
                if (randomValue > AttackChance)
                    herbivore.Die();
                EatCalled = true;
            }

            public override Animal Create(Cell cell)
            {
                return new CarnivoreMock(cell, RandomNumberGenerator);
            }

        }

        public class AnimalCreatorMock : IAnimalCreator 
        {
            private readonly IRandomNumberGenerator randomNumberGenerator;

            public AnimalCreatorMock(IRandomNumberGenerator randomNumberGenerator)
            {
                this.randomNumberGenerator = randomNumberGenerator;
            }

            public Animal Create(Cell cell)
            {
                var type = randomNumberGenerator.GetBetween(0, 2);
                if (type == 0)
                    return new CarnivoreMock(cell, randomNumberGenerator);
                else if (type == 1)
                    return new HerbivoreMock(cell, randomNumberGenerator);

                throw new NotSupportedException($"Unsupported animal type {type}");
            }
        }


        [Fact]
        public void Constructor()
        {
            //Arrange + Act + Assert
            var animalCreatorMock = new AnimalCreatorMock(Dice.Shared);
            Assert.Throws<ArgumentNullException>(() => new World<Animal, Animal>(1, 2, null, animalCreatorMock));
            Assert.Throws<ArgumentNullException>(() => new World<Animal, Animal>(1, 2, RandomNumberGeneratorMock.ZeroDice, null));
            Assert.Throws<ArgumentOutOfRangeException>(() => new World<Animal, Animal>(0, 2, RandomNumberGeneratorMock.ZeroDice, animalCreatorMock));
            Assert.Throws<ArgumentOutOfRangeException>(() => new World<Animal, Animal>(-1, 2, RandomNumberGeneratorMock.ZeroDice, animalCreatorMock));
            Assert.Throws<ArgumentOutOfRangeException>(() => new World<Animal, Animal>(2, 0, RandomNumberGeneratorMock.ZeroDice, animalCreatorMock));
            Assert.Throws<ArgumentOutOfRangeException>(() => new World<Animal, Animal>(2, -1, RandomNumberGeneratorMock.ZeroDice, animalCreatorMock));

        }

        [Fact]
        public void World_Create_Expected_Cells()
        {
            //Arrange
            var animalCreatorMock = new AnimalCreatorMock(Dice.Shared);
            int expectedCellsCount = 4;
            var testCell = new Cell(new Position(1, 1));
            //Act
            var world = new World<Animal, Animal>(2, 2, RandomNumberGeneratorMock.ZeroDice, animalCreatorMock);
            //Assert
            Assert.Equal(expectedCellsCount, world.Cells.Count());
        }

        [Fact]
        public void World_Create_Given_Number_Of_Animals()
        {
            //Arrange
            var animalCreatorMock = new AnimalCreatorMock(Dice.Shared);
            int expectedAnimalsCount = 4;
            //Act
            var world = new World<Animal, Animal>(2, 4, RandomNumberGeneratorMock.ZeroDice, animalCreatorMock);
            //Assert
            Assert.Equal(expectedAnimalsCount, world.Animals.Count());
        }

        [Fact]
        public void World_Cycle_Call_Animal_Couple_And_Eat_Methods()
        {
            var animalCreatorMock = new AnimalCreatorMock(Dice.Shared);
            var world = new World<CarnivoreMock, HerbivoreMock>(2, 4, RandomNumberGeneratorMock.ZeroDice, animalCreatorMock);
            var herbivore = world.Herbivores.First();
            world.Cycle();

            Assert.True(herbivore.EatCalled);
            Assert.True(herbivore.CouplingCalled);

        }
    }
}
