using System;
using System.Collections.Generic;
using System.Linq;
using MingStar.SimUniversity.ConsoleController.View;
using MingStar.SimUniversity.Contract;
using MingStar.SimUniversity.Game.Move;
using MingStar.Utilities;

namespace MingStar.SimUniversity.ConsoleController
{
    public class GameController
    {
        private readonly IPlayer[] _players;
        private Game.Game _game;
        private DateTime _startTime;

        public GameController(Game.Game game, params IPlayer[] players)
        {
            _players = players;
            Game = game;
        }

        public ConsoleViewer ConsoleViewer { get; private set; }

        public Game.Game Game
        {
            get { return _game; }
            set
            {
                _game = value;
                ConsoleViewer = new ConsoleViewer(_game);
                if (_players == null || _players.Length != _game.NumberOfUniversities)
                {
                    throw new ArgumentException("Number of players does not match number of universities in the game");
                }
                Game.HasHumanPlayer = _players.Any(player => player is HumanConsolePlayer);
            }
        }

        //internal void Reset()
        //{
        //    Game.Reset();
        //}

        public int Run()
        {
            _startTime = DateTime.Now;
            bool askSamePlayer;
            while (!Game.HasWinner())
            {
                Console.Title = string.Format("Round {0}, Turn: {1}", Game.Round, _game.CurrentTurn);
                if (Game.HasHumanPlayer)
                {
                    ConsoleViewer.PrintGame();
                }
                askSamePlayer = true;
                while (askSamePlayer)
                {
                    IPlayer currentPlayer = _players[Game.CurrentUniversityIndex];
                    List<IPlayerMove> moves = currentPlayer.MakeMoves(Game);
                    if (moves == null)
                    {
                        continue;
                    }
                    foreach (IPlayerMove move in moves)
                    {
                        var iMove = move as IProbabilityPlayerMove;
                        if (iMove != null)
                        {
                            iMove.IsDeterminated = false;
                        }
                        if (Game.IsLegalMove(move))
                        {
                            Game.ApplyMove(move);
                            ColorConsole.WriteLineIf(Game.HasHumanPlayer, GameConsoleColor.Move, move);
                            askSamePlayer = false;
                            if (move is EndTurn)
                            {
                                break;
                            }
                        }
                        else
                        {
                            if (Game.HasHumanPlayer)
                            {
                                ColorConsole.WriteLine(ConsoleColor.Red,
                                                       "Illegal Move '{0}' for player {1}",
                                                       move,
                                                       Game.CurrentUniversity);
                            }
                            askSamePlayer = true;
                            break;
                        }
                    }
                }
            }
            // now the game has a winner
            Console.Title = string.Format("Round {0}, Winner: {1}!", Game.Round, _game.CurrentUniversityColor);
            if (Game.HasHumanPlayer)
            {
                PrintFinalResult();
            }
            return Game.CurrentUniversityIndex;
        }

        public void PrintFinalResult()
        {
            ConsoleViewer.PrintGame();
            ColorConsole.WriteLine(ConsoleColor.Magenta, "Round {0}, turn {1}: the game has a winner!", Game.Round,
                                   Game.CurrentTurn);
            ColorConsole.WriteLine(ConsoleColor.Green, "Total time taken: {0}", (DateTime.Now - _startTime));
            Game.GameStats.PrintDiceRolls();
        }
    }
}