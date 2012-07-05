using System;

namespace MingStar.SimUniversity.Game
{
    public interface IViewer
    {
        void PrintGame();
        void PrintStats();
        void PrintFinalResult(TimeSpan timeTaken);

        void PrintTitle();

        void PrintLegalMove(Contract.IPlayerMove move);

        void PrintIllegalMove(Contract.IPlayerMove move);
    }
}