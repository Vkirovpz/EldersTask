using System;
using System.Numerics;
using System.Threading;
using System.Threading.Channels;
using TestElders.Models;

public class Program
{
    public static void Main(string[] args)
    {
        var animalCreator = new AnimalCreator(Dice.Shared);
        var world = new World<Carnivore, Herbivore>(1, 8, Dice.Shared, animalCreator);
        while (world.CanCycle)
        {
            world.Cycle();
        }

        Console.WriteLine("Game over");
    }
}