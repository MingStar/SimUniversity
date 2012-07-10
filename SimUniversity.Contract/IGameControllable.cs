namespace MingStar.SimUniversity.Contract
{
    public interface IGameControllable
    {
        void UndoMove();
        void ApplyMove(IPlayerMove move);
    }
}