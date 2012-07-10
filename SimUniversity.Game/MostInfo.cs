using MingStar.SimUniversity.Contract;

namespace MingStar.SimUniversity.Game
{
    public class MostInfo : IMostInfo
    {
        public int Threshold { get; private set; }
        public IUniversity University { get; private set; }
        public int Number { get; private set; }

        public MostInfo(int threshold) : this(null, 0, threshold)
        {
        }

        private MostInfo(IUniversity uni, int number, int threshold)
        {
            University = uni;
            Number = number;
            Threshold = threshold;
        }

        public IMostInfo GetMore(IUniversity uni, int number)
        {
            if (number >= Threshold && number > Number)
            {
                return new MostInfo(uni, number, Threshold);
            }
            return this;
        }
    }
}