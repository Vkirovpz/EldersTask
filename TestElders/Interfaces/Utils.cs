using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestElders.Helpers
{
    internal class Utils
    {
        internal static int GetRandomNumber(int min, int max)
        {
            var random = new Random();
            var retVal = random.Next(min, max);

            return retVal;
        }
    }
}
