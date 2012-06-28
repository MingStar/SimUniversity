using MingStar.SimUniversity.Contract;

namespace MingStar.SimUniversity.Game.Move
{
    public class TradingMove : IPlayerMove
    {
        private readonly StudentGroup[] _needed;

        public TradingMove(DegreeType tradeOut, int outNumber, DegreeType tradeIn)
        {
            TradeOut = tradeOut;
            OutQuantity = outNumber;
            TradeIn = tradeIn;
            if (TradeIn == DegreeType.None)
            {
                while (true)
                {
                    TradeIn = (DegreeType) (RandomGenerator.Next(Constants.RealDegrees.Length) + 1);
                    if (TradeIn != TradeOut)
                    {
                        break;
                    }
                }
            }
            _needed = new[]
                          {
                              new StudentGroup(TradeOut, OutQuantity)
                          };
        }

        public DegreeType TradeOut { get; private set; }
        public int OutQuantity { get; private set; }
        public DegreeType TradeIn { get; private set; }

        #region IPlayerMove Members

        public StudentGroup[] StudentsNeeded
        {
            get { return _needed; }
        }

        public void ApplyTo(IGame game)
        {
            game.TradeInStudent(TradeIn);
        }

        public void Undo(IGame game)
        {
            game.UnTradeInStudent(TradeIn);
        }

        public bool IsLegalToApply(IGame game)
        {
            return true;
        }

        #endregion

        public override string ToString()
        {
            if (TradeIn == DegreeType.None)
            {
                return string.Format("Exchange {0} {1} students for any 1 student", OutQuantity, TradeOut);
            }
            else
            {
                return string.Format("Exchange {0} {1} students for 1 {2} student", OutQuantity, TradeOut, TradeIn);
            }
        }
    }
}