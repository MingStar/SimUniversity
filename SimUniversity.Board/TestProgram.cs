using System;
using MingStar.SimUniversity.Board.Boards;

namespace MingStar.SimUniversity.Board
{
    internal static class TestProgram
    {
        private static void Main(string[] args)
        {
            var board = new SettlerBeginnerBoard();
            board.PrintToConsole();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        public static void PrintToConsole(this Board board)
        {
            foreach (Hexagon hex in board.GetHexagons())
            {
                Console.WriteLine("{0} \t{1}", hex, hex.Adjacent);
            }
            Console.WriteLine("# Vertices: " + Vertex.TotalCount);
            Console.WriteLine("# Edges: " + Edge.TotalCount);
        }
    }
}