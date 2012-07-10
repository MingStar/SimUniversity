namespace MingStar.SimUniversity.Contract
{
    public interface IGameControllable
    {
        void UndoMove();
        void ApplyMove(IPlayerMove move);
        bool HasHumanPlayer { get; set; } //FIXME: not to set the human player
    }
}