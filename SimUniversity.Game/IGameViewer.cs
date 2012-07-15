using System;
using MingStar.SimUniversity.Contract;

namespace MingStar.SimUniversity.Game
{
    public interface IGameViewer
    {
        void PrintGame();
        void PrintStats();
        void PrintRoundResult(TimeSpan timeTaken);
        void PrintTitle();
        void PrintLegalMove(IPlayerMove move);
        void PrintIllegalMove(IPlayerMove move);
        void SetGame(Game game);
    }
}