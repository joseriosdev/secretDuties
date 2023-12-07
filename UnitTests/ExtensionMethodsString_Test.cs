using DeacomDutiesExercise.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    public class ExtensionMethodsString_Test
    {
        [Theory]
        [InlineData("20 8 5 19 21 14 19 5 20 19 5 20 19 1 20 20 23 5 12 22 5 15 3 12 15 3 11", "The sunset sets at twelve o' clock.")]
        [InlineData("20 8 5 14 1 18 23 8 1 12 2 1 3 15 14 19 1 20 13 9 4 14 9 7 8 20", "The narwhal bacons at midnight.")]
        public void AlphabetPosition_Test(string expected, string input)
        {
            Assert.Equal(expected, input.AlphabetPosition());
        }

        [Theory]
        [InlineData("variableN", "variable-n")]
        [InlineData("DayOff", "Day_Off")]
        [InlineData("dataSciFi", "data-Sci-Fi")]
        public void ToCamelCase_Test(string expected, string input)
        {
            Assert.Equal(expected, input.ToCamelCase());
        }

        [Theory]
        [InlineData(3, "adddaaloom")]
        [InlineData(4, "fkdjkfjdffdjfdjfffdf")]
        [InlineData(0, "fr")]
        public void DuplicateCount_Test(int expected, string input)
        {
            Assert.Equal(expected, input.DuplicateCount());
        }

        [Theory]
        [InlineData(7, "asgdkasli")]
        [InlineData(4, "vfreer")]
        [InlineData(6, "lodkfdfdbbfslok")]
        public void LengthOfLongestSubstring_Test(int expected, string input)
        {
            Assert.Equal(expected, input.LengthOfLongestSubstring());
        }

        [Theory]
        [InlineData("amazing", "*m*z*ng", "aai")]
        [InlineData("grace", "gr*c*", "ae")]
        [InlineData("saviour", "s*v***r", "aiou")]
        public void Uncensor_Test(string expected, string input, string vow)
        {
            Assert.Equal(expected, input.Uncensor(vow));
        }

        [Theory]
        [InlineData(true, "bobboy")]
        [InlineData(false, "dddddd")]
        [InlineData(true, "reppel")]
        public void AlmostPalindrome_Test(bool expected, string input)
        {
            Assert.Equal(expected, input.AlmostPalindrome());
        }
    }
}
