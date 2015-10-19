namespace NewsSystem.Common.RandomGenerators
{
    using System;

    public static class NumberGenerator
    {
        private static Random random = new Random();

        public static int RandomNumber(int minValue, int maxValue)
        {
            return random.Next(minValue, maxValue + 1);
        }
    }
}
