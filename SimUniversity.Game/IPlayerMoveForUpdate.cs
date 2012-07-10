using MingStar.SimUniversity.Contract;

namespace MingStar.SimUniversity.Game
{
    public interface IPlayerMoveForUpdate : IPlayerMove
    {
        void ApplyTo(Game game);
        void Undo(Game game);
        bool IsLegalToApply(Game game);
    }
}