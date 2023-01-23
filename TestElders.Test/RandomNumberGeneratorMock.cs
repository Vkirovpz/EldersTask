using TestElders.Models;

namespace TestElders.Test;

public class RandomNumberGeneratorMock : IRandomNumberGenerator
{
    public static readonly RandomNumberGeneratorMock ZeroDice = new RandomNumberGeneratorMock(0);
    public static readonly RandomNumberGeneratorMock OneDice = new RandomNumberGeneratorMock(1);
    public static readonly RandomNumberGeneratorMock OneHundredDice = new RandomNumberGeneratorMock(100);

    public RandomNumberGeneratorMock(int value)
    {
        Value = value;
    }

    public int Value { get; }

    public int GetBetween(int min, int max) => Value;
}


