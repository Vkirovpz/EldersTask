using System;
using TestElders.Models;
using static TestElders.Test.WorldTests;

namespace TestElders.Test;


public class CarnivoreTests
{
    [Fact]
    public void Constructor()
    {
        //Arrange + Act + Assert
        var cell = new Cell(new Position(2, 2));
        Assert.Throws<ArgumentNullException>(() => new Carnivore(cell, null));
        Assert.Throws<ArgumentNullException>(() => new Carnivore(null, new Dice()));
    }

    [Fact]
    public void Carnivore_Move_Properly_To_New_Cell()
    {
        //Arrange
        var actualCell = new Cell(new Position(2, 2));
        var expectedCell = new Cell(new Position(2, 1));
        var animal = new Carnivore(actualCell, Dice.Shared);
        //Act
        animal.Move(expectedCell);
        //Assert
        Assert.Equal(animal.Cell, expectedCell);
    }

    [Fact]
    public void Carnivore_Die_Remove_It_From_Cell_Animals()
    {
        //Arrange
        var cell = new Cell(new Position(2, 2));
        var animal = new Carnivore(cell, new Dice());
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
        var animal = new Carnivore(cell, new Dice());
        //Act + Assert
        Assert.Throws<ArgumentNullException>(() => animal.Move(new Cell(null)));
    }

    [Fact]
    public void Carnivore_Eat_Removes_EatenHerbivore_From_Cell()
    {
        //Arrange
        var dice = RandomNumberGeneratorMock.ZeroDice;
        var cell = new Cell(new Position(2, 2));
        var carnivore = new Carnivore(cell, Dice.Shared);
        var herbivore = new Herbivore(cell, Dice.Shared);
        cell.Spawn(carnivore);
        cell.Spawn(herbivore);
        //Act
        carnivore.Eat();
        //Assert
        Assert.DoesNotContain(herbivore, cell.Animals);
    }

    [Fact]
    public void Carnivore_Insufficient_Chance_To_Eat_Dont_Removes_Herbivore_From_Cell()
    {
        //Arrange
        var cell = new Cell(new Position(2, 2));
        var carnivore = new CarnivoreMock(cell, RandomNumberGeneratorMock.ZeroDice);
        var herbivore = new HerbivoreMock(cell, RandomNumberGeneratorMock.ZeroDice);
        cell.Visit(carnivore);
        cell.Visit(herbivore);
        //Act

        carnivore.Eat();
        //Assert
        Assert.Contains(herbivore, cell.Animals);
    }

    [Fact]
    public void Female_Carnivore_Success_Couple_Add_New_Carnivore_To_Cell()
    {
        //Arrange      
        var cell = new Cell(new Position(2, 2));
        var maleAnimal = new Carnivore(cell, RandomNumberGeneratorMock.ZeroDice);
        var femaleAnimal = new Carnivore(cell, RandomNumberGeneratorMock.OneDice);
        cell.Visit(maleAnimal);
        cell.Visit(femaleAnimal);
        //Act
        femaleAnimal.Couple();
        //Assert
        Assert.Equal(3, cell.Animals.Count());
    }

    [Fact]
    public void Male_Carnivore_Success_Couple_Dont_Add_New_Herbivore_To_Cell()
    {
        //Arrange      
        var cell = new Cell(new Position(2, 2));
        var maleAnimal = new Carnivore(cell, RandomNumberGeneratorMock.ZeroDice);
        var femaleAnimal = new Carnivore(cell, RandomNumberGeneratorMock.OneDice);
        cell.Visit(maleAnimal);
        cell.Visit(femaleAnimal);
        //Act
        maleAnimal.Couple();
        //Assert
        Assert.Equal(2, cell.Animals.Count());
    }

    [Fact]
    public void Carnivore_Insufficient_Chance_To_Couple_Dont_Add_New_Carnivore_To_Cell()
    {
        //Arrange      
        var dice = RandomNumberGeneratorMock.OneHundredDice;
        var cell = new Cell(new Position(2, 2));
        var maleAnimal = new Carnivore(cell, RandomNumberGeneratorMock.ZeroDice);
        var femaleAnimal = new Carnivore(cell, RandomNumberGeneratorMock.OneDice);
        cell.Spawn(maleAnimal);
        cell.Spawn(femaleAnimal);
        //Act
        maleAnimal.Couple();
        //Assert
        Assert.Equal(2, cell.Animals.Count());
    }

    [Fact]
    public void Carnivore_Cant_Couple_Herbivore_Dont_Add_New_Carnivore_To_Cell()
    {
        //Arrange      
        var dice = RandomNumberGeneratorMock.ZeroDice;
        var cell = new Cell(new Position(2, 2));
        var carnivore = new Carnivore(cell, RandomNumberGeneratorMock.ZeroDice);
        var herbivore = new Herbivore(cell, RandomNumberGeneratorMock.ZeroDice);
        cell.Spawn(carnivore);
        cell.Spawn(herbivore);
        //Act
        carnivore.Couple();
        //Assert
        Assert.Equal(2, cell.Animals.Count());
    }

    [Fact]
    public void Carnivore_Cant_Couple_With_Same_Gender_Dont_Add_New_Carnivore_To_Cell()
    {
        //Arrange      
        var dice = RandomNumberGeneratorMock.ZeroDice;
        var cell = new Cell(new Position(2, 2));
        var maleAnimal = new Carnivore(cell, dice);
        var otherMaleAnimal = new Carnivore(cell, dice);
        cell.Spawn(maleAnimal);
        cell.Spawn(otherMaleAnimal);
        //Act
        maleAnimal.Couple();
        //Assert
        Assert.Equal(2, cell.Animals.Count());
    }
}
