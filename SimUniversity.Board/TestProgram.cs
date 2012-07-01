using System;
using System.Linq;
using MingStar.SimUniversity.Board.Constructor;

namespace MingStar.SimUniversity.Board
{
    internal static class TestProgram
    {
        private static void Main(string[] args)
        {
            var boardConstructor = new SettlerBeginnerBoardConstructor();
            boardConstructor.Board.PrintToConsole();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        public static void PrintToConsole(this Board board)
        {
            foreach (var hex in board.GetHexagons())
            {
                Console.WriteLine("{0} \t{1}", hex, hex.Adjacent);
            }
            Console.WriteLine("# Vertices: " + board.GetVertices().Count());
            Console.WriteLine("# Edges: " + board.GetEdges().Count());
        }
    }
}