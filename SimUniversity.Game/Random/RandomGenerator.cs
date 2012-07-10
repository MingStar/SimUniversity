namespace MingStar.SimUniversity.Game.Random
{
    public static class RandomGenerator
    {
        private static readonly System.Random _random = new System.Random();

        public static int Next(int maxValue)
        {
            return _random.Next(maxValue);
        }

        public static double NextDouble()
        {
            return _random.NextDouble();
        }
    }
}