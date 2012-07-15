﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using MingStar.SimUniversity.Contract;
using MingStar.SimUniversity.Game.Move;

namespace MingStar.SimUniversity.Game
{
    public class GameController
    {
        private readonly IPlayer[] _players;
        private DateTime _startTime;

        public GameController(IGameViewer viewer, Game game, bool hasHumanPlayer, params IPlayer[] players)
        {
            Viewer = viewer;
            Viewer.SetGame(game);
            _players = players;
            Game = game;
            if (_players == null || _players.Length != Game.NumberOfUniversities)
            {
                throw new ArgumentException("Number of players does not match number of universities in the game");
            }
            Game.HasHumanPlayer = hasHumanPlayer;
            Debug.Assert(Game.Board.IsLocked);
        }

        public IGameViewer Viewer { get; private set; }
        public Game Game { get; private set; }

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
            Viewer.PrintRoundResult(DateTime.Now - _startTime);
            return Game.CurrentUniversityIndex;
        }
    }
}