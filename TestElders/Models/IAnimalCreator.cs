using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestElders.Models
{
    public interface IAnimalCreator
    {
        Animal GetAnimal(string type, Cell cell, Gender gender);
    }
}
