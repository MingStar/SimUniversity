namespace MingStar.SimUniversity.Contract
{
    public interface IGameControllable
    {
        GamePhase CurrentPhase { get; }
        bool HasWinner();
        bool IsLegalMove(IPlayerMove move);
        void ApplyMove(IPlayerMove move);
        EnrolmentInfo DiceRoll(int diceTotal);
        void NextTurn();
    }
}