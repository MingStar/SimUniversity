using System;
using System.Collections.Generic;
using System.Linq;
using MingStar.SimUniversity.AI.Evaluation;
using MingStar.SimUniversity.AI.Learning;
using MingStar.SimUniversity.AI.Player;
using MingStar.SimUniversity.Contract;
using MingStar.SimUniversity.Game;
using MingStar.Utilities;
using MingStar.Utilities.Linq;
using log4net;

namespace MingStar.SimUniversity.ConsoleUI
{
    public class SimplexLearning
    {
        public const string FileName = "LearningResult.xml";

        private static readonly ILog _log = LogManager.GetLogger(typeof (SimplexLearning));
        private readonly IPredefinedBoardConstructor _boardConstructor;
        private readonly IGameViewer _gameViewer;
        private int _roundsToWinInTournament;
        private SimplexLearnedScores _learnedScores;
        private DateTime _learningStartedDT;
        private int _totalTournamentsToRun;
        private int _touranmentCount;

        public SimplexLearning(IGameViewer gameGameViewer, IPredefinedBoardConstructor boardConstructor)
        {
            _gameViewer = gameGameViewer;
            _boardConstructor = boardConstructor;
        }

        public void Learn(int maxEvaluationCount, int roundsToWinInTournament)
        {
            _roundsToWinInTournament = roundsToWinInTournament;
            _learnedScores = LoadSimplexConstants();
            _totalTournamentsToRun = maxEvaluationCount + 1 + _learnedScores.NumberOfParametersToLearn;
            _touranmentCount = 0;
            _log.Info("Start simplex learning");
            _learningStartedDT = DateTime.Now;
            RegressionResult result = NelderMeadSimplex.Regress(_learnedScores.ToSimplexConstants(), 0.01, maxEvaluationCount, RunTournament);
            _learnedScores.FromResult(result.Constants);
            LogInfo("GOT LEARNING RESULT: {0}", _learnedScores);
            _learnedScores.Save(FileName);
            _log.Info("Finish simplex learning");
        }

        private static void LogInfo(string format, params object[] args)
        {
            _log.InfoFormat(format, args);
            ColorConsole.WriteLine(ConsoleColor.Magenta, format, args);
        }

        private static SimplexLearnedScores LoadSimplexConstants()
        {
            try
            {
                return SimplexLearnedScores.Load(FileName);
            }
            catch
            {
                return new SimplexLearnedScores();
            }
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
            while (round < _roundsToWinInTournament)
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
            _touranmentCount++;
            var remainingTimeSpan = GetEstimatedFinishedTime();
            LogInfo("Got score: {0}. Challenger won {1} rounds. Time taken this tournament: {2}." + 
                " Total time taken: {3}. Estimated finished time: {4} ({5} to go)",
                totalScore, 
                tournamentResult.ChallengerWinningCount,
                DateTime.Now - startedTime,
                DateTime.Now - _learningStartedDT,
                DateTime.Now + remainingTimeSpan,
                remainingTimeSpan);
            return -totalScore; // return negative for function minimisation
        }

        private TimeSpan GetEstimatedFinishedTime()
        {
            var averageMilliseconds = (DateTime.Now - _learningStartedDT).TotalMilliseconds/_touranmentCount;
            var remainingTouraments = _totalTournamentsToRun - _touranmentCount;
            return TimeSpan.FromMilliseconds(averageMilliseconds*remainingTouraments);
        }
    }
}