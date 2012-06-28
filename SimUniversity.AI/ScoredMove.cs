using MingStar.SimUniversity.Contract;

namespace MingStar.SimUniversity.AI
{
    public class ScoredMove
    {
        public ScoredMove(IPlayerMove move, double score)
        {
            Move = move;
            Score = score;
        }

        public IPlayerMove Move { get; private set; }
        public double Score { get; private set; }
    }
}