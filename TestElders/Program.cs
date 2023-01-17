using System;
using System.Numerics;
using System.Threading;
using System.Threading.Channels;
using TestElders.Helpers;
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

        //Console.Clear();
        //World.PrintMatrix(world);
        //Console.WriteLine("Press any key to start");
        //Console.ReadKey(true);
        //Console.Clear();

        //while (allHerbivores.Count > 0)
        //{
        //    //World.PrintMatrix(world);
        //    //AnimalHelper.Move(allHerbivores, allCarnivores, matrix, killChance);
        //    int milliseconds = 500;
        //    Thread.Sleep(milliseconds);
        //    Console.Clear();
        //}


    }

}