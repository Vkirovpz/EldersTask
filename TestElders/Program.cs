using System;
using System.Numerics;
using System.Threading;
using System.Threading.Channels;
using TestElders.Models;

public class Program
{
    public static void Main(string[] args)
    {
        var world = new World();
        while (world.Herbivores.Any())
        {
            world.Cycle();
        }

        Console.WriteLine("Game over");
    }

}