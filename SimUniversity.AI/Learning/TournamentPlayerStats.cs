using System;
using MingStar.Utilities;

namespace MingStar.SimUniversity.AI.Learning
{
    public class TournamentPlayerStats
    {
        public string PlayerName { get; set; }
        public int WinCount { get; set; }

        public void HasWon()
        {
            ++WinCount;
        }

        public void PrintToConsole()
        {
            ColorConsole.WriteLine(ConsoleColor.Cyan,
                                   "Player '{0}' has won {1} times",
                                   PlayerName, WinCount);
        }
    }
}