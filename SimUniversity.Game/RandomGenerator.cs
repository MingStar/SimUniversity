using System;

namespace MingStar.SimUniversity.Game
{
    public static class RandomGenerator
    {
        #region Public static Properties

        private static readonly Random _randomGenerator = new Random();

        #endregion

        public static Random Random
        {
            get { return _randomGenerator; }
        }

        public static int Next(int maxValue)
        {
            return _randomGenerator.Next(maxValue);
        }
    }
}