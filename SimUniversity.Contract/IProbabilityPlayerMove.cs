namespace MingStar.SimUniversity.Contract
{
    public interface IProbabilityPlayerMove : IPlayerMove
    {
        IProbabilityPlayerMove[] AllProbabilityMoves { get; }
        bool IsDeterminated { get; set; }
        double? Probability { get; }
    }
}