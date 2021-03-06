﻿using System;
using System.Collections.Generic;
using MingStar.SimUniversity.Contract;

namespace MingStar.SimUniversity.ConsoleUI
{
    public static class ConsoleViewerColor
    {
        public static Dictionary<DegreeType, ConsoleColor> Degree;
        public static ConsoleColor Move = ConsoleColor.Green;

        static ConsoleViewerColor()
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