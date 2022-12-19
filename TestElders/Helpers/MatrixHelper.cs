using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestElders.Models;

namespace TestElders.Helpers
{
    internal static class MatrixHelper
    {
        internal static char[,] SetMatrix(List<Herbivore> herbivores, List<Carnivore> carnivores, int matrixLength)
        {
            var matrix = new char[matrixLength, matrixLength];
            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    matrix[row, col] = '▒';
                }
            }

            foreach (var herbivore in herbivores)
            {
                bool repeat = true;
                while (repeat)
                {
                    herbivore.Row = Utils.GetRandomNumber(0, matrix.GetLength(0));
                    herbivore.Col = Utils.GetRandomNumber(0, matrix.GetLength(1));
                    if (matrix[herbivore.Row, herbivore.Col] == '▒' && matrix[herbivore.Row, herbivore.Col] != 'x')
                    {
                        matrix[herbivore.Row, herbivore.Col] = herbivore.Char;
                        repeat = false;
                    }
                }
            }
            foreach (var carnivore in carnivores)
            {
                bool repeat = true;
                while (repeat)
                {
                    carnivore.Row = Utils.GetRandomNumber(0, matrix.GetLength(0));
                    carnivore.Col = Utils.GetRandomNumber(0, matrix.GetLength(1));
                    if (matrix[carnivore.Row, carnivore.Col] == '▒' && matrix[carnivore.Row, carnivore.Col] != 'o')
                    {
                        matrix[carnivore.Row, carnivore.Col] = carnivore.Char;
                        repeat = false;
                    }
                }
            }
            return matrix;
        }

        public static void PrintMatrix(char[,] matrix)
        {
            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    Console.Write("{0}", matrix[row, col]);
                }
                Console.WriteLine();
            }
        }
    }
}
