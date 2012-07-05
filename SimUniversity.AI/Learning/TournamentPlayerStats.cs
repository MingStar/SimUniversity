using System;
using MingStar.Utilities;

namespace MingStar.SimUniversity.AI.Learning
{
    public class TournamentPlayerStats
    {
        public string PlayerName;
        public int RealWinCount;
        public int WinCount;

        public void HasWon(bool really)
        {
            ++WinCount;
            if (really)
            {
                ++RealWinCount;
            }
        }

        public void PrintToConsole()
        {
            ColorConsole.WriteLine(ConsoleColor.Cyan,
                                   "Player '{0}' has won {1} times, and really won {2} times",
                                   PlayerName, WinCount, RealWinCount);
        }
    }
}