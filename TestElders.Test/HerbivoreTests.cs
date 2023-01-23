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
            Assert.Throws<ArgumentNullException>(() => new Herbivore(null, RandomNumberGeneratorMock.ZeroDice));
        }

        [Fact]
        public void Herbivore_Move_Properly_To_New_Cell()
        {
            //Arrange
            var actualCell = new Cell(new Position(2, 2));
            var expectedCell = new Cell(new Position(2, 1));
            var animal = new Herbivore(actualCell, Dice.Shared);
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
            var animal = new Herbivore(cell, RandomNumberGeneratorMock.ZeroDice);
            cell.Visit(animal);
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
            var animal = new Herbivore(cell, new Dice());
            //Act + Assert
            Assert.Throws<ArgumentNullException>(() => animal.Move(new Cell(null)));
        }

        [Fact]
        public void Female_Herbivore_Success_Couple_Add_New_Herbivore_To_Cell()
        {
            //Arrange      
            var cell = new Cell(new Position(2, 2));
            var maleAnimal = new Herbivore(cell, RandomNumberGeneratorMock.ZeroDice);
            var femaleAnimal = new Herbivore(cell, RandomNumberGeneratorMock.OneDice);
            cell.Visit(maleAnimal);
            cell.Visit(femaleAnimal);
            //Act
            femaleAnimal.Couple();
            //Assert
            Assert.Equal(3, cell.Animals.Count());
        }

        [Fact]
        public void Male_Herbivore_Success_Couple_Dont_Add_New_Herbivore_To_Cell()
        {
            //Arrange      
            var cell = new Cell(new Position(2, 2));
            var maleAnimal = new Herbivore(cell, RandomNumberGeneratorMock.ZeroDice);
            var femaleAnimal = new Herbivore(cell, RandomNumberGeneratorMock.OneDice);
            cell.Visit(maleAnimal);
            cell.Visit(femaleAnimal);
            //Act
            maleAnimal.Couple();
            //Assert
            Assert.Equal(2, cell.Animals.Count());
        }

        [Fact]
        public void Herbivore_Insufficient_Chance_To_Couple_Dont_Add_New_Herbivore_To_Cell()
        {
            //Arrange      
            var dice = RandomNumberGeneratorMock.OneHundredDice;
            var cell = new Cell(new Position(2, 2));
            var maleAnimal = new Herbivore(cell, RandomNumberGeneratorMock.ZeroDice);
            var femaleAnimal = new Herbivore(cell, RandomNumberGeneratorMock.OneDice);
            cell.Spawn(maleAnimal);
            cell.Spawn(femaleAnimal);
            //Act
            maleAnimal.Couple();
            //Assert
            Assert.Equal(2, cell.Animals.Count());
        }

        [Fact]
        public void Herbivore_Cant_Couple_Carnivore_Dont_Add_New_Herbivore_To_Cell()
        {
            //Arrange      
            var dice = RandomNumberGeneratorMock.ZeroDice;
            var cell = new Cell(new Position(2, 2));
            var herbivore = new Herbivore(cell, RandomNumberGeneratorMock.OneDice);
            var carnivore = new Carnivore(cell, RandomNumberGeneratorMock.ZeroDice);
            cell.Spawn(herbivore);
            cell.Spawn(carnivore);
            //Act
            herbivore.Couple();
            //Assert
            Assert.Equal(2, cell.Animals.Count());
        }

        [Fact]
        public void Herbivore_Cant_Couple_With_Same_Gender_Dont_Add_New_Carnivore_To_Cell()
        {
            //Arrange      
            var dice = RandomNumberGeneratorMock.ZeroDice;
            var cell = new Cell(new Position(2, 2));
            var maleAnimal = new Herbivore(cell, dice);
            var otherMaleAnimal = new Herbivore(cell, dice);
            cell.Spawn(maleAnimal);
            cell.Spawn(otherMaleAnimal);
            //Act
            maleAnimal.Couple();
            //Assert
            Assert.Equal(2, cell.Animals.Count());
        }
    }
}
