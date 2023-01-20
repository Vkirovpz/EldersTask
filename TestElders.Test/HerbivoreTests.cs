using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestElders.Models;

namespace TestElders.Test
{
    public class HerbivoreTests
    {
        [Fact]
        public void Constructor()
        {
            //Arrange + Act + Assert
            var cell = new Cell(new Position(2, 2));
            Assert.Throws<ArgumentNullException>(() => new Herbivore(cell, null));
            Assert.Throws<ArgumentNullException>(() => new Herbivore(null, Gender.Male));
        }

        [Fact]
        public void Herbivore_Move_Properly_To_New_Cell()
        {
            //Arrange
            var actualCell = new Cell(new Position(2, 2));
            var expectedCell = new Cell(new Position(2, 1));
            var animal = new Herbivore(actualCell, Gender.Random(new Dice()));
            //Act
            animal.Move(expectedCell);
            //Assert
            Assert.Equal(animal.Cell, expectedCell);
        }

        [Fact]
        public void Herbivore_Die_Remove_It_From_Cell_Animals()
        {
            //Arrange
            var cell = new Cell(new Position(2, 2));
            var animal = new Herbivore(cell, Gender.Random(new Dice()));
            cell.Spawn(animal);
            //Act
            animal.Die();
            //Assert
            Assert.Empty(cell.Animals);
        }

        [Fact]
        public void Animal_Move_To_Null_Cell_Throws()
        {
            //Arrange
            var cell = new Cell(new Position(2, 2));
            var animal = new Herbivore(cell, Gender.Random(new Dice()));
            //Act + Assert
            Assert.Throws<ArgumentNullException>(() => animal.Move(new Cell(null)));
        }

        [Fact]
        public void Herbivore_Succes_Coupling_Add_New_Herbivore_To_Cell()
        {
            //Arrange      
            var dice = RandomNumberGeneratorMock.ZeroDice;
            var cell = new Cell(new Position(2, 2));
            var maleAnimal = new Herbivore(cell, Gender.Male);
            var femaleAnimal = new Herbivore(cell, Gender.Female);
            cell.Spawn(maleAnimal);
            cell.Spawn(femaleAnimal);
            //Act
            maleAnimal.Coupling(dice);
            //Assert
            Assert.Equal(3, cell.Animals.Count());
        }

        [Fact]
        public void Herbivore_Insufficient_Chance_To_Coupling_Dont_Add_New_Herbivore_To_Cell()
        {
            //Arrange      
            var dice = RandomNumberGeneratorMock.OneHundredDice;
            var cell = new Cell(new Position(2, 2));
            var maleAnimal = new Herbivore(cell, Gender.Male);
            var femaleAnimal = new Herbivore(cell, Gender.Female);
            cell.Spawn(maleAnimal);
            cell.Spawn(femaleAnimal);
            //Act
            maleAnimal.Coupling(dice);
            //Assert
            Assert.Equal(2, cell.Animals.Count());
        }

        [Fact]
        public void Herbivore_Cant_Coupling_Carnivore_Dont_Add_New_Herbivore_To_Cell()
        {
            //Arrange      
            var dice = RandomNumberGeneratorMock.ZeroDice;
            var cell = new Cell(new Position(2, 2));
            var maleAnimal = new Herbivore(cell, Gender.Male);
            var otherMaleAnimal = new Carnivore(cell, Gender.Female);
            cell.Spawn(maleAnimal);
            cell.Spawn(otherMaleAnimal);
            //Act
            maleAnimal.Coupling(dice);
            //Assert
            Assert.Equal(2, cell.Animals.Count());
        }
    }
}
