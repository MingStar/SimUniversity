﻿using System;
using System.Collections.Generic;
using System.Linq;
using MingStar.SimUniversity.AI.Player;
using MingStar.SimUniversity.Contract;
using MingStar.SimUniversity.Game;
using MingStar.SimUniversity.Game.Move;
using MingStar.Utilities;

namespace MingStar.SimUniversity.ConsoleUI
{
    public class ConsoleHumanPlayer : IPlayer
    {
        private static readonly char[] s_spliters = new[] {' '};
        public GameController GameController { get; set; }

        #region IPlayer Members

        public List<IPlayerMove> MakeMoves(IGame game)
        {
            PrintAllMoves(game);
            while (true)
            {
                Console.WriteLine("Please type instruction:");
                Console.WriteLine("(i=InternetLink, c=Campus, u=Upgrade, x=eXchange, t=TryStartUp, Enter=end turn)");
                Console.WriteLine("(s=Save, l=Load, -=Undo, p=stats, /=suggest, .=use suggest, m=moves)");
                var instruction = Console.ReadLine();
                try
                {
                    List<IPlayerMove> moves = GetMoves(game, instruction);
                    if (moves != null)
                    {
                        return moves;
                    }
                    DoSystemAction(game, instruction);
                    //return null;
                }
                catch (Exception ex)
                {
                    ColorConsole.WriteLine(ConsoleColor.Red,
                                           "Unknown command: '{0}'. Details error: {1}",
                                           instruction,
                                           ex.Message);
                }
                // otherwise, continue;
            }
        }

        public string Name
        {
            get { return "Human Console"; }
        }

        #endregion

        private static void PrintAllMoves(IGame game)
        {
            var allMoves = game.GenerateAllMoves();
            if (game.CurrentPhase == GamePhase.Play || allMoves.Count() < 10)
            {
                PrintMoves(ConsoleColor.Yellow, allMoves);
            }
            else // game.CurrentPhase == Setup1 & Setup 2
            {
                ColorConsole.WriteLine(ConsoleColor.Yellow, "Example move: {0}", ImprovedEMN.Instance.MakeMoves(game)[0]);
            }
        }

        private static void PrintMoves(ConsoleColor color, IEnumerable<IPlayerMove> allMoves)
        {
            foreach (IPlayerMove possibleMove in allMoves)
            {
                ColorConsole.WriteLine(color, possibleMove.ToString());
            }
        }

        private void DoSystemAction(IGame game, string instruction)
        {
            string command = instruction.Trim().ToLower();
            switch (command)
            {
                case "s":
                    ColorConsole.WriteLine(ConsoleColor.Yellow, "This feature is coming soon.");
                    break;
                case "l":
                    ColorConsole.WriteLine(ConsoleColor.Yellow, "This feature is coming soon.");
                    break;
                case "-":
                    game.UndoMove();
                    GameController.Viewer.PrintGame();
                    break;
                case "p":
                    GameController.Viewer.PrintStats();
                    break;
                case "/":
                    PrintMoves(ConsoleColor.Magenta, ImprovedEMN.Instance.MakeMoves(game));
                    break;
                case "m":
                    PrintAllMoves(game);
                    break;
                default:
                    throw new Exception("Not a valid system command");
            }
        }

        private List<IPlayerMove> GetMoves(IGame game, string instruction)
        {
            if (string.IsNullOrEmpty(instruction))
            {
                return new List<IPlayerMove> {new EndTurn()};
            }
            var moves = new List<IPlayerMove>();
            string[] array = instruction.Split(s_spliters);
            string command = array[0].ToLower();
            switch (command)
            {
                case "i":
                    moves.Add(new BuildLinkMove(
                                  new EdgePosition(ToPosition(array[1], array[2]),
                                                   ToEdgeOrientation(array[3]))));
                    break;
                case "c":
                    moves.Add(new BuildCampusMove(
                                  new VertexPosition(ToPosition(array[1], array[2]),
                                                     ToVertexOrientation(array[3])),
                                  CampusType.Traditional));
                    break;
                case "u":
                    moves.Add(new BuildCampusMove(
                                  new VertexPosition(ToPosition(array[1], array[2]),
                                                     ToVertexOrientation(array[3])),
                                  CampusType.Super));
                    break;
                case "x":
                    moves.Add(new TradingMove(ToDegreeType(array[2]),
                                              int.Parse(array[1]),
                                              ToDegreeType(array[3])
                                  ));
                    break;
                case "t":
                    if (array.Length > 1)
                    {
                        throw new Exception("Too many arguments.");
                    }
                    moves.Add(new TryStartUpMove());
                    break;
                case ".":
                    moves.Add(ImprovedEMN.Instance.MakeMoves(game)[0]);
                    break;
                case "..":
                    moves = ImprovedEMN.Instance.MakeMoves(game);
                    break;
                default:
                    return null;
            }
            return moves;
        }

        private Position ToPosition(string strX, string strY)
        {
            return new Position(int.Parse(strX), int.Parse(strY));
        }

        private DegreeType ToDegreeType(string s)
        {
            switch (s.ToLower())
            {
                case "b":
                    return DegreeType.Brick;
                case "g":
                    return DegreeType.Grain;
                case "o":
                    return DegreeType.Ore;
                case "s":
                    return DegreeType.Sheep;
                case "w":
                    return DegreeType.Wood;
            }
            throw new ArgumentException("Unknown Degree Type: " + s);
        }

        private VertexOrientation ToVertexOrientation(string s)
        {
            switch (s.ToLower())
            {
                case "tl":
                    return VertexOrientation.TopLeft;
                case "l":
                    return VertexOrientation.Left;
                case "bl":
                    return VertexOrientation.BottomLeft;
                case "tr":
                    return VertexOrientation.TopRight;
                case "br":
                    return VertexOrientation.BottomRight;
                case "r":
                    return VertexOrientation.Right;
            }
            throw new ArgumentException("Unknown Vertex Orientation: " + s);
        }

        private EdgeOrientation ToEdgeOrientation(string s)
        {
            switch (s.ToLower())
            {
                case "t":
                    return EdgeOrientation.Top;
                case "tl":
                    return EdgeOrientation.TopLeft;
                case "bl":
                    return EdgeOrientation.BottomLeft;
                case "b":
                    return EdgeOrientation.Bottom;
                case "br":
                    return EdgeOrientation.BottomRight;
                case "tr":
                    return EdgeOrientation.TopRight;
            }
            throw new ArgumentException("Unknown Edge Orientation: " + s);
        }
    }
}