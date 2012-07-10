using System;
using System.Collections.Generic;
using System.Linq;
using MingStar.SimUniversity.AI.Evaluation;
using MingStar.SimUniversity.AI.Player;
using MingStar.SimUniversity.Contract;
using MingStar.SimUniversity.Game;
using MingStar.SimUniversity.Game.Random;
using MingStar.Utilities;
using MingStar.Utilities.Linq;
using log4net;

namespace MingStar.SimUniversity.AI.Learning
{
    public class Learning
    {
        private const string FileName = "LearningResult.xml";
        private static readonly ILog _log = LogManager.GetLogger(typeof (Learning));
        private static bool IsFirstCall = true;
        private readonly IPredefinedBoardConstructor _boardConstructor;
        private readonly IGameViewer _gameViewer;

        public Learning(IGameViewer gameGameViewer, IPredefinedBoardConstructor boardConstructor)
        {
            _gameViewer = gameGameViewer;
            _boardConstructor = boardConstructor;
        }

        public void Learn(int rounds)
        {
            _log.Info("Start to do simplex learning");
            RegressionResult result = NelderMeadSimplex.Regress(LoadSimplexConstants(), 0.01, rounds, RunTournament);
            SaveResult(result);
            _log.Info("Finish to do simplex learning");
        }

        private void SaveResult(RegressionResult result)
        {
            LogDoubleArray("Got result:", result.Constants);
            var s = new SimplexLearnedScores();
            s.FromResult(result.Constants);
            s.Save(FileName);
        }

        private static void LogDoubleArray(string prefix, IEnumerable<double> array)
        {
            _log.InfoFormat("{0} [{1}]",
                            prefix,
                            string.Join(", ", (from item in array select item.ToString()).ToArray())
                );
        }


        private static SimplexConstant[] LoadSimplexConstants()
        {
            SimplexLearnedScores s;
            try
            {
                s = SimplexLearnedScores.Load(FileName);
            }
            catch
            {
                s = new SimplexLearnedScores();
            }
            return s.ToSimplexConstants();
        }

        private double RunTournament(double[] values)
        {
            if (IsFirstCall)
            {
                IsFirstCall = false;
                return 0.0;
            }
            LogDoubleArray("Got parameters:", values);
            var learnedScores = new SimplexLearnedScores();
            learnedScores.FromResult(values);
            var stats = new Dictionary<string, TournamentPlayerStats>();
            const int numPlayers = 2;
            int round = 0;
            double totalScore = 0;
            const int maxTotalWinningRound = 7;
            while (true)
            {
                ++round;
                int challengerIndex = RandomGenerator.Next(numPlayers);
                var game = new Game.Game(_boardConstructor.ConstructBoard(), numPlayers) {Round = round};
                var _improvedEMM_AIPlayer_normal = new ImprovedEMN(new GameScores());
                var _improvedEMM_AIPlayer_expansion = new ImprovedEMN(learnedScores);
                var players = new IPlayer[numPlayers];
                players.Fill(_improvedEMM_AIPlayer_normal);
                string challengerName = _improvedEMM_AIPlayer_expansion.Name;
                players[challengerIndex] = _improvedEMM_AIPlayer_expansion;
                for (int j = 0; j < numPlayers; ++j)
                {
                    string name = players[j].Name;
                    if (!stats.ContainsKey(name))
                    {
                        stats[name] = new TournamentPlayerStats
                                          {
                                              PlayerName = name
                                          };
                    }
                }
                var controller = new GameController(_gameViewer, game, false, players);
                int winnerIndex = controller.Run();
                TournamentPlayerStats stat = stats[players[winnerIndex].Name];
                ColorConsole.WriteLine(ConsoleColor.Yellow,
                                       ">>> University {0}, AI player '{1}' has won. <<<",
                                       controller.Game.Universities[winnerIndex].Color,
                                       stat.PlayerName
                    );
                bool areDiceFair = controller.Game.GameStats.AreDiceFair();
                stat.HasWon(areDiceFair);
                if (areDiceFair)
                {
                    totalScore += GetChallengerScore(controller.Game, challengerIndex);
                }
                int totalRealWinCount = 0;
                foreach (TournamentPlayerStats statForPrint in stats.Values)
                {
                    statForPrint.PrintToConsole();
                    totalRealWinCount += statForPrint.RealWinCount;
                }
                if (totalRealWinCount >= maxTotalWinningRound)
                {
                    double winningRate = stats[challengerName].RealWinCount - (double) maxTotalWinningRound/numPlayers;
                    totalScore += winningRate*200.0;
                    break;
                }
            }
            _log.InfoFormat("Got Score: {0}", totalScore);
            return -totalScore; // return negative for function minimisation
        }


        private static double GetChallengerScore(IGame game, int challengerIndex)
        {
            double totalScore = 0.0;
            IUniversity challengerUni = game.Universities[challengerIndex];
            int challengerScore = game.GetScore(challengerUni);
            // score difference to other players
            foreach (var uni in game.Universities)
            {
                if (uni == challengerUni)
                {
                    continue;
                }
                int otherScore = game.GetScore(uni);
                int diff = challengerScore - otherScore;
                int diffSquared = diff*diff;
                totalScore += diff > 0 ? diffSquared : -diffSquared; // to retain the sign;
            }
            // winning score
            return totalScore;
        }
    }
}