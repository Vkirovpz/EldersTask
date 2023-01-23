using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestElders.Models;

namespace TestElders.Test
{
    public class CellTests
    {

        [Fact]
        public void Constructor()
        {
            //Arrange + Act + Assert
            Assert.Throws<ArgumentNullException>(() => new Cell(null));
        }
        [Fact]
        public void Adding_Null_Argument_To_CellMethods_Throws()
        {
            //Arrange + Act + Assert
            var cell = new Cell(new Position(2, 2));
            Assert.Throws<ArgumentNullException>(() => cell.Spawn(null));
            Assert.Throws<ArgumentNullException>(() => cell.Visit(null));
            Assert.Throws<ArgumentNullException>(() => cell.Leave(null));
        }

        [Fact]
        public void CellSpawn_Add_Animal()
        {
            //Arrange
            var cell = new Cell(new Position(2, 2));
            var cellInitialAnimalsCount = cell.Animals.Count();
            var animalCreator = new AnimalCreator(RandomNumberGeneratorMock.ZeroDice);
            //Act
            cell.Spawn(animalCreator);
            //Assert
            Assert.Single(cell.Animals);
        }

        [Fact]
        public void CellLeave_Remove_Animal()
        {
            //Arrange
            var cell = new Cell(new Position(2, 2));
            var animal = new Carnivore(cell, RandomNumberGeneratorMock.ZeroDice);
            //Act
            cell.Leave(animal);
            //Assert
            Assert.Empty(cell.Animals);
        }

        [Fact]
        public void CellVisit_Add_Animal()
        {
            //Arrange
            var actualCell = new Cell(new Position(2, 2));
            var expectedlCell = new Cell(new Position(2, 1));
            var animal = new Carnivore(actualCell, RandomNumberGeneratorMock.ZeroDice);
            actualCell.Spawn(animal);
            //Act
            expectedlCell.Visit(animal);
            //Assert
            Assert.Contains(animal,expectedlCell.Animals);
        }
    }
}
