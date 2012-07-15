using System;
using System.Collections.Generic;
using System.Linq;

namespace MingStar.SimUniversity.AI.Learning
{
    public class TournamentResult
    {
        private int? minRoundIndex;
        private int _minRoundScore = int.MaxValue;

        private int? maxRoundIndex;
        private int _maxRoundScore = int.MinValue;

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
                + CalculateScoreFromWinning()
                + ChallengerWinningCount;
        }

        /// <summary>
        /// Take away one max point and one min point to minimise variance
        /// </summary>
        /// <returns></returns>
        public int CalculateScoreFromRounds()
        {
            int sum = 0;
            int index = 0;
            foreach (var result in RoundResults)
            {
                var roundTotal = result.RoundTotalScore;
                sum += roundTotal;
                if (roundTotal > _maxRoundScore)
                {
                    _maxRoundScore = roundTotal;
                    maxRoundIndex = index;
                }
                if (roundTotal < _minRoundScore)
                {
                    _minRoundScore = roundTotal;
                    minRoundIndex = index;
                }
                index++;
            }
            return sum - _maxRoundScore - _minRoundScore;
        }

        /// <summary>
        /// Take away one max poin and one min point
        /// </summary>
        /// <returns></returns>
        public double CalculateScoreFromWinning()
        {
            int numberOfPlayers = RoundResults.First().OtherPlayerScore.Count() + 1;
            var tuple = TakeAwayMinAndMax();
            int realRoundCount = tuple.Item1;
            int realWinningCount = tuple.Item2;
            double average = (double)realRoundCount / numberOfPlayers;
            double winningRate = (realWinningCount - average) * 10;
            var winningSquared = winningRate * winningRate;
            return winningRate > 0 ? winningSquared : -winningSquared; // retain the sign;
        }

        private Tuple<int, int> TakeAwayMinAndMax()
        {
            // take away min and max
            if (!minRoundIndex.HasValue)
            {
                CalculateScoreFromRounds();
            }
            var realWinningCount = ChallengerWinningCount;
            int realRoundCount = RoundResults.Count();
            if (minRoundIndex.HasValue)
            {
                realRoundCount--;
                if (_roundResults[minRoundIndex.Value].HasChallengerWin)
                {
                    realWinningCount--;
                }
            }
            if (maxRoundIndex.HasValue)
            {
                realRoundCount--;
                if (_roundResults[maxRoundIndex.Value].HasChallengerWin)
                {
                    realWinningCount--;
                }
            }
            return new Tuple<int, int>(realRoundCount, realWinningCount);
        }
    }

    public class RoundResult
    {
        public int ChallengerScore { get; private set; }
        public IEnumerable<int> OtherPlayerScore { get; private set; }
        public bool HasChallengerWin { get; private set; }
        public int RoundTotalScore { get; private set; }

        public RoundResult(int challengerScore, bool hasChallengerWin, IEnumerable<int> otherPlayerScore)
        {
            ChallengerScore = challengerScore;
            HasChallengerWin = hasChallengerWin;
            OtherPlayerScore = otherPlayerScore;
            RoundTotalScore = GetRoundTotal();
        }

        private int GetRoundTotal()
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
