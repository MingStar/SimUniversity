using MingStar.SimUniversity.Contract;
using MingStar.SimUniversity.Game.Random;

namespace MingStar.SimUniversity.Game.Move
{
    public class TradingMove : IPlayerMoveForUpdate, ITradingMove
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
                    TradeIn = (DegreeType) (RandomGenerator.Next(GameConstants.RealDegrees.Length) + 1);
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

        #region IPlayerMoveForUpdate Members

        public StudentGroup[] StudentsNeeded
        {
            get { return _needed; }
        }

        public void ApplyTo(Game game)
        {
            game.TradeInStudent(TradeIn);
        }

        public void Undo(Game game)
        {
            game.UnTradeInStudent(TradeIn);
        }

        public bool IsLegalToApply(Game game)
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
            return string.Format("Exchange {0} {1} students for 1 {2} student", OutQuantity, TradeOut, TradeIn);
        }
    }
}