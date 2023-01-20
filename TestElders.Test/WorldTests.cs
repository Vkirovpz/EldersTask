//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using TestElders.Models;

//namespace TestElders.Test
//{
//    public class WorldTests
//    {
//        internal class HerbivoreMock : Animal
//        {
//            public bool CouplingCalled { get; set; }
//            public bool EatCalled { get; set; }

//            public HerbivoreMock(Cell cell, Gender gender) : base(cell, gender) { }

//            public override void Coupling(IRandomNumberGenerator rng)
//            {
//                CouplingCalled = true;
//            }

//            public override void Eat(IRandomNumberGenerator rng)
//            {
//                EatCalled = true;
//            }
//        }

//        internal class CarnivoreMock : Animal
//        {
//            public bool CouplingCalled { get; set; }
//            public bool EatCalled { get; set; }

//            public CarnivoreMock(Cell cell, Gender gender) : base(cell, gender) { }

//            public override void Coupling(IRandomNumberGenerator rng)
//            {
//                CouplingCalled = true;
//            }

//            public override void Eat(IRandomNumberGenerator rng)
//            {
//                EatCalled = true;
//            }
//        }


//        [Fact]
//        public void Constructor()
//        {
//            //Arrange + Act + Assert
//            Assert.Throws<ArgumentNullException>(() => new World<Animal, Animal>(1, 2, null));
//            Assert.Throws<ArgumentOutOfRangeException>(() => new World<Animal, Animal>(0, 2, RandomNumberGeneratorMock.ZeroChance));
//            Assert.Throws<ArgumentOutOfRangeException>(() => new World<Animal, Animal>(-1, 2, RandomNumberGeneratorMock.ZeroChance));
//            Assert.Throws<ArgumentOutOfRangeException>(() => new World<Animal, Animal>(2, 0, RandomNumberGeneratorMock.ZeroChance));
//            Assert.Throws<ArgumentOutOfRangeException>(() => new World<Animal, Animal>(2, -1, RandomNumberGeneratorMock.ZeroChance));

//        }

//        [Fact]
//        public void World_Create_Expected_Cells()
//        {
//            //Arrange
//            int expectedCellsCount = 4;
//            var testCell = new Cell(new Position(1, 1));
//            //Act
//            var world = new World<Animal, Animal>(2, 2, RandomNumberGeneratorMock.ZeroChance);
//            //Assert
//            Assert.Equal(expectedCellsCount, world.Cells.Count());
//        }

//        [Fact]
//        public void World_Create_Given_Number_Of_Animals()
//        {
//            //Arrange
//            int expectedAnimalsCount = 4;
//            //Act
//            var world = new World<Animal, Animal>(2, 4, RandomNumberGeneratorMock.ZeroChance);
//            //Assert
//            Assert.Equal(expectedAnimalsCount, world.Animals.Count());
//        }

//    }
//}
