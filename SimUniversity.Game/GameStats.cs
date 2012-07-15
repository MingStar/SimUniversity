using System;
using System.Collections.Generic;
using System.Linq;
using MingStar.SimUniversity.Contract;
using MingStar.Utilities;
using MingStar.Utilities.Generics;
using Enumerable = System.Linq.Enumerable;

namespace MingStar.SimUniversity.Game
{
    public class GameStats
    {
        public const double PearsonThreshold = 11.07;

        public GameStats()
        {
            DiceRolls = new Dictionary<int, int>();
            DiceRolls.InitialiseKeys(2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12);
        }

        public Dictionary<int, int> DiceRolls { get; private set; }
        public int TotalDiceRoll { get; private set; }

        public void DiceRolled(int total)
        {
            ++DiceRolls[total];
            ++TotalDiceRoll;
        }

        public void UndoDiceRolled(int total)
        {
            --DiceRolls[total];
            --TotalDiceRoll;
        }

        public void PrintDiceRolls()
        {
            ColorConsole.WriteLine(ConsoleColor.DarkYellow, "Total dice rolls: {0}", TotalDiceRoll);
            ColorConsole.WriteLine(ConsoleColor.DarkYellow, "Distribution: [{0}]",
                string.Join(", ", Enumerable.Range(2, 11).Select(i => string.Format("{0}: {1}", i, DiceRolls[i])))
                );
            PrintVariance();
        }

        private void PrintVariance()
        {
            double pearsonError = GetPearsonError();
            bool isFair = pearsonError < PearsonThreshold;
            ColorConsole.WriteLine(ConsoleColor.DarkYellow,
                                   "Pearson error rate: {0} {1} 11.07, dice is {2}fair.",
                                   pearsonError, isFair ? "<" : ">", isFair ? "" : "not ");
        }

        public double GetPearsonError()
        {
            double error = 0.0;
            for (int i = 2; i <= 12; ++i)
            {
                double expectedProbability = GameConstants.DiceRollNumber2Chance[i] / GameConstants.Chance.TotalDiceRoll;
                double expectedNumer = TotalDiceRoll*expectedProbability;
                double diffNumber = expectedNumer - DiceRolls[i];
                error += (diffNumber*diffNumber)/expectedNumer;
            }
            return error;
        }

        public bool AreDiceFair()
        {
            return GetPearsonError() < PearsonThreshold;
        }
    }
}