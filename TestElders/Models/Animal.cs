using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TestElders.Models
{
    public abstract class Animal
    {
        public int Row { get; set; }
        public int Col { get; set; }

        public virtual char Char { get;}

        public void Move(char[,] matrix, string direction)
        {
            if (direction == "right" && this.Col <= matrix.GetLength(1) - 2)
            {
                matrix[this.Row, this.Col] = '▒';
                this.Col++;
            }
            else if (direction == "left" && this.Col > 0)
            {
                matrix[this.Row, this.Col] = '▒';
                this.Col--;
            }
            else if (direction == "up" && this.Row > 0)
            {
                matrix[this.Row, this.Col] = '▒';
                this.Row--;
            }
            else if (direction == "down" && this.Row <= matrix.GetLength(0) - 2)
            {
                matrix[this.Row, this.Col] = '▒';
                this.Row++;
            }
        }
    }
}
