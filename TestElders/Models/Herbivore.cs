﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestElders.Models
{
    public class Herbivore : Animal
    {
        public Herbivore(Cell cell) : base(cell) { }

        public override void Eat() { }
    }
}
