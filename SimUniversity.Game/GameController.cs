using System;
using System.Linq;
using MingStar.SimUniversity.Contract;
using MingStar.SimUniversity.Game.Move;

namespace MingStar.SimUniversity.Game
{
    public class GameController
    {
        private readonly IPlayer[] _players;
        private DateTime _startTime;
        public IViewer Viewer { get; private set; }
        public Game Game { get; private set; }

        public GameController(IViewer viewer, Game game, bool hasHumanPlayer, params IPlayer[] players)
        {
            Viewer = viewer;
            _players = players;
            Game = game;
            if (_players == null || _players.Length != Game.NumberOfUniversities)
            {
                throw new ArgumentException("Number of players does not match number of universities in the game");
            }
            Game.HasHumanPlayer = hasHumanPlayer;
        }

        public int Run()
        {
            _startTime = DateTime.Now;
            while (!Game.HasWinner())
            {
                Viewer.PrintTitle();
                if (Game.HasHumanPlayer)
                {
                    Viewer.PrintGame();
                }
                bool askSamePlayer = true;
                while (askSamePlayer)
                {
                    var currentPlayer = _players[Game.CurrentUniversityIndex];
                    var moves = currentPlayer.MakeMoves(Game);
                    if (moves == null)
                    {
                        continue;
                    }
                    foreach (var move in moves)
                    {
                        var iMove = move as IProbabilityPlayerMove;
                        if (iMove != null)
                        {
                            iMove.IsDeterminated = false;
                        }
                        if (Game.IsLegalMove(move))
                        {
                            Game.ApplyMove(move);
                            Viewer.PrintLegalMove(move);
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
                                Viewer.PrintIllegalMove(move);
                            }
                            askSamePlayer = true;
                            break;
                        }
                    }
                }
            }
            // now the game has a winner
            Viewer.PrintFinalResult(DateTime.Now - _startTime);
            return Game.CurrentUniversityIndex;
        }
    }
}