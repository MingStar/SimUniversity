using System;
using System.Collections.Generic;
using System.Linq;

namespace MingStar.SimUniversity.AI.Learning
{
    public class TournamentResult
    {
        private readonly List<RoundResult> _roundResults = new List<RoundResult>();

        public IEnumerable<RoundResult> RoundResults
        {
            get { return _roundResults; }
        }

        public void AddRound(RoundResult roundResult)
        {
            _roundResults.Add(roundResult);
        }

        public int ChallengerWinningCount
        {
            get { return RoundResults.Count(r => r.HasChallengerWin); }
        }

        public double CalculateTotalScore()
        {
            return CalculateScoreFromRounds()
                + CalculateScoreFromWinning();
        }

        /// <summary>
        /// Take away one max point and one min point to minimise variance
        /// </summary>
        /// <returns></returns>
        public int CalculateScoreFromRounds()
        {
            int sum = 0;
            int max = int.MinValue;
            int min = int.MaxValue;
            foreach (var result in RoundResults)
            {
                var roundTotal = result.GetRoundTotal();
                sum += roundTotal;
                max = roundTotal > max ? roundTotal : max;
                min = roundTotal < min ? roundTotal : min;
            }
            return sum - max - min;
        }

        public double CalculateScoreFromWinning()
        {
            int numberOfPlayers = RoundResults.First().OtherPlayerScore.Count() + 1;
            double average = (double)RoundResults.Count() / numberOfPlayers;
            double winningRate = (ChallengerWinningCount - average) * 10;
            var winningSquared = winningRate * winningRate;
            return winningRate > 0 ? winningSquared : -winningSquared; // retain the sign;
        }
    }

    public class RoundResult
    {
        public int ChallengerScore { get; private set; }
        public IEnumerable<int> OtherPlayerScore { get; private set; }
        public bool HasChallengerWin { get; private set; }

        public RoundResult(int challengerScore, bool hasChallengerWin, IEnumerable<int> otherPlayerScore)
        {
            ChallengerScore = challengerScore;
            HasChallengerWin = hasChallengerWin;
            OtherPlayerScore = otherPlayerScore;
        }

        public int GetRoundTotal()
        {
            int totalScore = 0;
            // score difference to other players
            foreach (var otherScore in OtherPlayerScore)
            {
                int diff = ChallengerScore - otherScore;
                int diffSquared = diff*diff;
                totalScore += diff > 0 ? diffSquared : -diffSquared; // to retain the sign;
            }
            // winning score
            return totalScore;            
        }
    }
}
