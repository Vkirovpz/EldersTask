using System;
using TestElders.Models;

namespace TestElders.Test;


public class CarnivoreTests
{


    [Fact]
    public void Constructor()
    {
        //Arrange + Act + Assert
        var cell = new Cell(new Position(2, 2));
        Assert.Throws<ArgumentNullException>(() => new Carnivore(cell, null));
        Assert.Throws<ArgumentNullException>(() => new Carnivore(null, Gender.Male));
    }

    [Fact]
    public void Carnivore_Move_Properly_To_New_Cell()
    {
        //Arrange
        var actualCell = new Cell(new Position(2, 2));
        var expectedCell = new Cell(new Position(2, 1));
        var animal = new Carnivore(actualCell, Gender.Random(new Dice()));
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
        var animal = new Carnivore(cell, Gender.Random(new Dice()));
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
        var animal = new Carnivore(cell, Gender.Random(new Dice()));
        //Act + Assert
        Assert.Throws<ArgumentNullException>(() => animal.Move(new Cell(null)));
    }

    [Fact]
    public void Carnivore_Eat_Removes_EatenHerbivore_From_Cell()
    {
        //Arrange
        var dice = RandomNumberGeneratorMock.ZeroDice;
        var cell = new Cell(new Position(2, 2));
        var carnivore = new Carnivore(cell, Gender.Random(new Dice()));
        var herbivore = new Herbivore(cell, Gender.Random(new Dice()));
        cell.Spawn(carnivore);
        cell.Spawn(herbivore);
        //Act
        carnivore.Eat(dice);
        //Assert
        Assert.DoesNotContain(herbivore, cell.Animals);
    }

    [Fact]
    public void Carnivore_Insufficient_Chance_To_Eat_Dont_Removes_Herbivore_From_Cell()
    {
        //Arrange
        var dice = RandomNumberGeneratorMock.OneHundredDice;
        var cell = new Cell(new Position(2, 2));
        var carnivore = new Carnivore(cell, Gender.Random(new Dice()));
        var herbivore = new Herbivore(cell, Gender.Random(new Dice()));
        cell.Spawn(carnivore);
        cell.Spawn(herbivore);
        //Act
        carnivore.Eat(dice);
        //Assert
        Assert.Contains(herbivore, cell.Animals);
    }

    [Fact]
    public void Carnivore_Succes_Coupling_Add_New_Carnivore_To_Cell()
    {
        //Arrange      
        var dice = RandomNumberGeneratorMock.ZeroDice;
        var cell = new Cell(new Position(2, 2));
        var maleAnimal = new Carnivore(cell, Gender.Male);
        var femaleAnimal = new Carnivore(cell, Gender.Female);
        cell.Spawn(maleAnimal);
        cell.Spawn(femaleAnimal);
        //Act
        maleAnimal.Coupling(dice);
        //Assert
        Assert.Equal(3, cell.Animals.Count());
    }

    [Fact]
    public void Carnivore_Insufficient_Chance_To_Coupling_Dont_Add_New_Carnivore_To_Cell()
    {
        //Arrange      
        var dice = RandomNumberGeneratorMock.OneHundredDice;
        var cell = new Cell(new Position(2, 2));
        var maleAnimal = new Carnivore(cell, Gender.Male);
        var femaleAnimal = new Carnivore(cell, Gender.Female);
        cell.Spawn(maleAnimal);
        cell.Spawn(femaleAnimal);
        //Act
        maleAnimal.Coupling(dice);
        //Assert
        Assert.Equal(2, cell.Animals.Count());
    }

    [Fact]
    public void Carnivore_Cant_Coupling_Herbivore_Dont_Add_New_Carnivore_To_Cell()
    {
        //Arrange      
        var dice = RandomNumberGeneratorMock.ZeroDice;
        var cell = new Cell(new Position(2, 2));
        var maleAnimal = new Carnivore(cell, Gender.Male);
        var otherMaleAnimal = new Herbivore(cell, Gender.Female);
        cell.Spawn(maleAnimal);
        cell.Spawn(otherMaleAnimal);
        //Act
        maleAnimal.Coupling(dice);
        //Assert
        Assert.Equal(2, cell.Animals.Count());
    }

    [Fact]
    public void Carnivore_Cant_Coupling_With_Same_Gender_Dont_Add_New_Carnivore_To_Cell()
    {
        //Arrange      
        var dice = RandomNumberGeneratorMock.ZeroDice;
        var cell = new Cell(new Position(2, 2));
        var maleAnimal = new Carnivore(cell, Gender.Male);
        var femaleAnimal = new Carnivore(cell, Gender.Male);
        cell.Spawn(maleAnimal);
        cell.Spawn(femaleAnimal);
        //Act
        maleAnimal.Coupling(dice);
        //Assert
        Assert.Equal(2, cell.Animals.Count());
    }
}

public class RandomNumberGeneratorMock : IRandomNumberGenerator
{
    public static readonly RandomNumberGeneratorMock ZeroDice = new RandomNumberGeneratorMock(0);
    public static readonly RandomNumberGeneratorMock OneHundredDice = new RandomNumberGeneratorMock(100);

    public RandomNumberGeneratorMock(int value)
    {
        Value = value;
    }

    public int Value { get; }

    public int GetBetween(int min, int max) => Value;
}


