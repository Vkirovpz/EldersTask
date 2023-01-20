using System;
using System.Numerics;
using System.Threading;
using System.Threading.Channels;
using TestElders.Models;

public class Program
{
    public static void Main(string[] args)
    {
        var dice = new Dice();
        var animalCreator = new AnimalCreator();
        var world = new World<Carnivore, Herbivore>(1, 8, dice, animalCreator);
        while (world.CanCycle)
        {
            world.Cycle();
        }

        Console.WriteLine("Game over");
    }
}