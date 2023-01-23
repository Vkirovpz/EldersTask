using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestElders.Models;

namespace TestElders.Test
{
    public class GenderTests
    {
        [Fact]
        public void Gender_Random_Test()
        {
            Assert.Throws<NotSupportedException>(() => Gender.Random(RandomNumberGeneratorMock.OneHundredDice));
        }

        [Fact]
        public void Male_Gender_Is_DIfferent_From_Female_Gender()
        {
            var maleGender = Gender.Random(RandomNumberGeneratorMock.ZeroDice);
            var femaleGender = Gender.Random(RandomNumberGeneratorMock.OneDice);

            Assert.NotEqual(femaleGender, maleGender);
        }
    }
}
