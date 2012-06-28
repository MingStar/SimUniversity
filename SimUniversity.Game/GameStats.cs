using System;
using System.Collections.Generic;
using MingStar.Utilities;
using MingStar.Utilities.Generics;

namespace MingStar.SimUniversity.Game
{
    public class GameStats
    {
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
            for (int i = 2; i <= 12; ++i)
            {
                ColorConsole.WriteLine(ConsoleColor.White, "{0}{1}: {2}",
                                       i < 10 ? " " : "",
                                       i,
                                       DiceRolls[i]
                    );
            }
            PrintVariance();
        }

        private void PrintVariance()
        {
            /*
            double error = 0.0;
            for (int i = 2; i <= 12; ++i)
            {
                double expectedProbability = GameConstants.HexID2Chance[i] / (double)GameConstants.TotalDiceRollChances;
                double actualProbability = DiceRolls[i] / (double)TotalDiceRoll;
                double diff = expectedProbability - actualProbability;
                error += (diff * diff) / expectedProbability;
            }
             */
            ColorConsole.WriteLine(ConsoleColor.DarkYellow, "Total dice rolls: {0}", TotalDiceRoll);
            ColorConsole.WriteLine(ConsoleColor.DarkYellow, "Pearson error rate: {0}", GetPearsonError());
            ColorConsole.WriteLine(ConsoleColor.DarkYellow, "Are dice fair? {0}", AreDiceFair());
            /*
            ConsoleHelper.WriteLine(ConsoleColor.DarkYellow, "Another error rate: {0}", error);
             */
        }

        public double GetPearsonError()
        {
            double error = 0.0;
            for (int i = 2; i <= 12; ++i)
            {
                double expectedProbability = GameConstants.HexID2Chance[i]/GameConstants.Chance.TotalDiceRoll;
                double expectedNumer = TotalDiceRoll*expectedProbability;
                double diffNumber = expectedNumer - DiceRolls[i];
                error += (diffNumber*diffNumber)/expectedNumer;
            }
            return error;
        }


        public bool AreDiceFair()
        {
            return GetPearsonError() < 11.07;
        }
    }
}