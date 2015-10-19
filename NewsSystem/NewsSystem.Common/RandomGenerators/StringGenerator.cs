using System;
using System.Text;
namespace NewsSystem.Common.RandomGenerators
{
    public static class StringGenerator
    {

        private static Random random = new Random();
        private static string Alphabet = "ABCDEFGHIJKLMNOPQRSTUWXYZabcdefghijklmnopqrstuwxyz";
        private static string AlphabetWithSpaces = "  ABCDEFGHIJKLMNOPQRSTUWXYZ  abcdefghijklmnopqrstuwxyz  ";

        public static string RandomStringWithoutSpaces(int minLength, int maxLength)
        {
            StringBuilder result = new StringBuilder();
            var length = NumberGenerator.RandomNumber(minLength, maxLength);

            for (int i = 0; i < length; i++)
            {
                result.Append(Alphabet[NumberGenerator.RandomNumber(0, Alphabet.Length - 1)]);
            }

            return result.ToString();
        }

        public static string RandomStringWithSpaces(int minLength, int maxLength)
        {
            StringBuilder result = new StringBuilder();
            var length = NumberGenerator.RandomNumber(minLength, maxLength);

            for (int i = 0; i < length; i++)
            {
                result.Append(AlphabetWithSpaces[NumberGenerator.RandomNumber(0, AlphabetWithSpaces.Length - 1)]);
            }

            return result.ToString();
        }

    }
}
