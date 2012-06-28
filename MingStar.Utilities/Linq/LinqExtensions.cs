using System;
using System.Collections.Generic;
using System.Linq;

namespace MingStar.Utilities.Linq
{
    public static class LinqExtensions
    {
        private static Random _randomGenerator;

        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (_randomGenerator == null)
            {
                _randomGenerator = new Random();
            }

            T[] array = source.ToArray();
            int index = array.Length;
            while (index > 1)
            {
                int otherIndex = _randomGenerator.Next(index);
                --index;
                T tmp = array[index];
                array[index] = array[otherIndex];
                array[otherIndex] = tmp;
            }
            return array;
        }


        public static void Fill<T>(this T[] source, T value)
        {
            if (source == null)
            {
                return;
            }
            for (int i = 0; i < source.Length; ++i)
            {
                source[i] = value;
            }
        }
    }
}