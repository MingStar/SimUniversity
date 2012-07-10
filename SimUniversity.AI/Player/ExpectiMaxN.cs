﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using MingStar.SimUniversity.AI.Evaluation;
using MingStar.SimUniversity.Contract;
using MingStar.SimUniversity.Game.Move;

namespace MingStar.SimUniversity.AI.Player
{
    public class ExpectiMaxN : IPlayer
    {
        protected GameEvaluation _gameEvaluation;

        public ExpectiMaxN() : this(new GameScores())
        {
        }

        public ExpectiMaxN(GameScores scores)
        {
            _gameEvaluation = new GameEvaluation(scores);
            SetupPlayer = new SetupPlayer(_gameEvaluation);
        }

        public SetupPlayer SetupPlayer { get; private set; }

        #region IPlayer Members

        public List<IPlayerMove> MakeMoves(IGame game)
        {
            if (game.CurrentPhase != GamePhase.Play)
            {
                return SetupPlayer.MakeMoves(game);
            }
            var theGame = (Game.Game) game;
            bool hasHuman = theGame.HasHumanPlayer;
            theGame.HasHumanPlayer = false;
            GameState scoredMoves = SearchBestMoves(theGame);
            List<IPlayerMove> moves = scoredMoves.GetMoveList();
            theGame.HasHumanPlayer = hasHuman;
            return moves;
        }

        public string Name
        {
            get { return "ExpectiMax N, with " + _gameEvaluation.Scores.GetType().Name; }
        }

        #endregion

        public void SetGameScores(GameScores scores)
        {
            _gameEvaluation.Scores = scores;
        }

        public virtual GameState SearchBestMoves(Game.Game game)
        {
            return SearchBestMoves(game, 3);
        }

        private GameState SearchBestMoves(Game.Game game, int depth)
        {
            if (game.HasWinner() || depth == 0)
            {
                return new GameState(Evaluate(game));
            }
            ReadOnlyCollection<IPlayerMove> allMoves = game.GenerateAllMoves();
            if (allMoves.Count == 1)
            {
                return new GameState(allMoves[0], Evaluate(game));
            }
            var bestMoves = new GameState(game.NumberOfUniversities, double.MinValue);
            int currentUniversityIndex = game.CurrentUniversityIndex;
            foreach (IPlayerMove move in allMoves)
            {
                if (move is BuildCampusMove || move is BuildLinkMove || move is TradingMove)
                {
                    game.ApplyMove(move);
                    GameState nextScoredMoves = SearchBestMoves(game, depth - 1);
                    game.UndoMove();
                    bestMoves.TakeIfBetter(nextScoredMoves, currentUniversityIndex, move);
                }
                else if (move is IProbabilityPlayerMove)
                {
                    var expectedScores = new GameState(game.NumberOfUniversities, 0);
                    foreach (IProbabilityPlayerMove possibleMove in ((IProbabilityPlayerMove) move).AllProbabilityMoves)
                    {
                        game.ApplyMove(possibleMove);
                        GameState nextScores = SearchBestMoves(game, depth - 1);
                        game.UndoMove();
                        for (int i = 0; i < expectedScores.Scores.Length; ++i)
                        {
                            expectedScores[i] += possibleMove.Probability.Value*nextScores[i];
                        }
                    }
                    bestMoves.TakeIfBetter(expectedScores, currentUniversityIndex, move);
                }
            }
            return bestMoves;
        }

        protected double[] Evaluate(IGame game)
        {
            return (from uni in game.Universities
                    select _gameEvaluation.Evaluate(game, uni)).ToArray();
        }
    }
}