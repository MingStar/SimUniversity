using System;
using System.Collections.Generic;
using MingStar.SimUniversity.Contract;

namespace MingStar.SimUniversity.ConsoleController.View
{
    public static class GameConsoleColor
    {
        public static Dictionary<DegreeType, ConsoleColor> Degree;
        public static ConsoleColor Move = ConsoleColor.Green;

        static GameConsoleColor()
        {
            Degree = new Dictionary<DegreeType, ConsoleColor>();
            Degree[DegreeType.Brick] = ConsoleColor.DarkYellow;
            Degree[DegreeType.Wood] = ConsoleColor.DarkGreen;
            Degree[DegreeType.Sheep] = ConsoleColor.Green;
            Degree[DegreeType.Ore] = ConsoleColor.DarkGray;
            Degree[DegreeType.Grain] = ConsoleColor.Yellow;
            Degree[DegreeType.None] = ConsoleColor.White;
        }
    }
}