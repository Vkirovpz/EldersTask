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
        Console.WriteLine("Select how many Herbivores to create : ");
        var herbivoresCount = int.Parse(Console.ReadLine());
        Console.WriteLine("Select how many Carnivores to create : ");
        var canrivoresCount = int.Parse(Console.ReadLine());
        Console.WriteLine("Select kill chance : ");
        var killChance = int.Parse(Console.ReadLine());
        Console.WriteLine("Select matrix length : ");
        var matrixLength = int.Parse(Console.ReadLine());
        (List<Carnivore> allCarnivores, List<Herbivore> allHerbivores) = AnimalHelper.InitAnimals(herbivoresCount, canrivoresCount);
        var matrix = MatrixHelper.SetMatrix(allHerbivores, allCarnivores, matrixLength);
        Console.Clear();
        MatrixHelper.PrintMatrix(matrix);
        Console.WriteLine("Press any key to start");
        Console.ReadKey(true);
        Console.Clear();

        while (allHerbivores.Count > 0)
        {
            MatrixHelper.PrintMatrix(matrix);
            AnimalHelper.Move(allHerbivores, allCarnivores, matrix, killChance);
            int milliseconds = 500;
            Thread.Sleep(milliseconds);
            Console.Clear();
        }
        MatrixHelper.PrintMatrix(matrix);
        Console.WriteLine();
        Console.WriteLine("All herbivores have been eaten! Game over.");
    }
      
}