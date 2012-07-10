using System;
using MingStar.Utilities;

namespace MingStar.SimUniversity.AI.Learning
{
    public class TournamentPlayerStats
    {
        public string PlayerName { get; set; }
        public int RealWinCount { get; set; }
        public int WinCount { get; set; }

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