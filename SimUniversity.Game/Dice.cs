using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MingStar.SimUniversity.Game
{
    public static class Dice
    {
        public static IDiceTotalRoll DiceTotalGenerator { get; set; }

        public static int GetDiceTotal()
        {
            if (DiceTotalGenerator != null)
            {
                return DiceTotalGenerator.GetNextDiceTotal();
            }
            else
            {
                return RandomGenerator.Next(6) + RandomGenerator.Next(6) + 2;
            }
        }
    }
}
