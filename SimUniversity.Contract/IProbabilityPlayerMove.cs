using System.Collections.Generic;

namespace MingStar.SimUniversity.Contract
{
    public interface IProbabilityPlayerMove : IPlayerMove
    {
        IEnumerable<IProbabilityPlayerMove> AllProbabilityMoves { get; }
        bool IsDeterminated { get; set; }
        double? Probability { get; }
    }
}