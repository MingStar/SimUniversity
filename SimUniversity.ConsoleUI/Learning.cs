﻿using System;
using System.Collections.Generic;
using System.Linq;
using MingStar.SimUniversity.AI.Evaluation;
using MingStar.SimUniversity.AI.Learning;
using MingStar.SimUniversity.AI.Player;
using MingStar.SimUniversity.Contract;
using MingStar.SimUniversity.Game;
using MingStar.SimUniversity.Game.Random;
using MingStar.Utilities;
using MingStar.Utilities.Linq;
using log4net;

namespace MingStar.SimUniversity.ConsoleUI
{
    public class Learning
    {
        public const string FileName = "LearningResult.xml";
        private static readonly ILog _log = LogManager.GetLogger(typeof (Learning));
        private readonly IPredefinedBoardConstructor _boardConstructor;
        private readonly IGameViewer _gameViewer;
        private int _roundsToWin;
        private SimplexLearnedScores _learnedScores;
        private DateTime _learningStartedDT;

        public Learning(IGameViewer gameGameViewer, IPredefinedBoardConstructor boardConstructor)
        {
            _gameViewer = gameGameViewer;
            _boardConstructor = boardConstructor;
        }

        public void Learn(int rounds, int roundsToWin)
        {
            _roundsToWin = roundsToWin;
            _log.Info("Start to do simplex learning");
            _learningStartedDT = DateTime.Now;
            RegressionResult result = NelderMeadSimplex.Regress(LoadSimplexConstants(), 0.01, rounds, RunTournament);
            _learnedScores.FromResult(result.Constants);
            LogInfo("GOT LEARNING RESULT: {0}", _learnedScores);
            _learnedScores.Save(FileName);
            _log.Info("Finish to do simplex learning");
        }

        private static void LogInfo(string format, params object[] args)
        {
            _log.InfoFormat(format, args);
            ColorConsole.WriteLine(ConsoleColor.Magenta, format, args);
        }

        private SimplexConstant[] LoadSimplexConstants()
        {
            try
            {
                _learnedScores = SimplexLearnedScores.Load(FileName);
            }
            catch
            {
                _learnedScores = new SimplexLearnedScores();
            }
            return _learnedScores.ToSimplexConstants();
        }

        private double RunTournament(double[] values)
        {
            DateTime startedTime = DateTime.Now;
            _learnedScores.FromResult(values);
            var stats = new Dictionary<string, TournamentPlayerStats>();
            const int numPlayers = 2;
            int round = 0;
            var tournamentResult = new TournamentResult();
            int challengerIndex = 0;
            while (round < _roundsToWin)
            {
                ++round;
                challengerIndex = (challengerIndex + 1) % numPlayers;
                var game = new Game.Game(_boardConstructor.ConstructBoard(), numPlayers) {Round = round};
                var _improvedEMM_AIPlayer_normal = new ImprovedEMN(new GameScores());
                var _improvedEMM_AIPlayer_expansion = new ImprovedEMN(_learnedScores);
                var players = new IPlayer[numPlayers];
                players.Fill(_improvedEMM_AIPlayer_normal);
                players[challengerIndex] = _improvedEMM_AIPlayer_expansion;
                for (var j = 0; j < numPlayers; ++j)
                {
                    var name = players[j].Name;
                    if (!stats.ContainsKey(name))
                    {
                        stats[name] = new TournamentPlayerStats
                                          {
                                              PlayerName = name
                                          };
                    }
                }
                var controller = new GameController(_gameViewer, game, false, players);
                var winnerIndex = controller.Run();
                var stat = stats[players[winnerIndex].Name];
                ColorConsole.WriteLine(ConsoleColor.Yellow,
                                       ">>> University {0}, AI player '{1}' has won. <<<",
                                       controller.Game.GetUniversityByIndex(winnerIndex).Color,
                                       stat.PlayerName
                    );
                stat.HasWon();
                var challengerUni = game.GetUniversityByIndex(challengerIndex);
                tournamentResult.AddRound(
                    new RoundResult(game.GetScore(challengerUni),
                                    winnerIndex == challengerIndex,
                                    game.Universities.Where(u => u != challengerUni).Select(game.GetScore)
                        ));
                foreach (var statForPrint in stats.Values)
                {
                    statForPrint.PrintToConsole();
                }
            }
            var totalScore = tournamentResult.CalculateTotalScore();
            LogInfo(_learnedScores.ToString());
            LogInfo("Got score: {0}. Challenger won {1} rounds. Time taken this round: {2}. Total time taken: {3}",
                totalScore, 
                tournamentResult.ChallengerWinningCount,
                DateTime.Now - startedTime,
                DateTime.Now - _learningStartedDT);
            return -totalScore; // return negative for function minimisation
        }
    }
}