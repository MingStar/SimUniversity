namespace MingStar.SimUniversity.Game
{
    public class MostInfo
    {
        public readonly int Threshold;

        public MostInfo(int threshold) : this(null, 0, threshold)
        {
        }

        private MostInfo(University uni, int number, int threshold)
        {
            University = uni;
            Number = number;
            Threshold = threshold;
        }

        public University University { get; private set; }
        public int Number { get; private set; }

        public MostInfo GetMore(University uni, int number)
        {
            if (number >= Threshold && number > Number)
            {
                return new MostInfo(uni, number, Threshold);
            }
            return this;
        }
    }
}