using System;
using System.Numerics;
using System.Threading;
using System.Threading.Channels;
using TestElders.Models;

public class Program
{
    public static void Main(string[] args)
    {
        var world = new World(3, 5);
        while (world.CanCycle)
        {
            world.Cycle();
        }

        Console.WriteLine("Game over");
    }
}