using System.Collections.Generic;
using System.Linq;
using MingStar.SimUniversity.Contract;

namespace MingStar.SimUniversity.AI.Evaluation
{
    public class GameEvaluation
    {
        public GameScores Scores { get; private set; }

        public GameEvaluation(GameScores scores)
        {
            Scores = scores;
        }

        public double Evaluate(IGame game, IUniversity uni)
        {
            double score = 0.0;
            int currentScore = game.GetScore(uni);

            if (currentScore >= game.Rules.WinningScore)
            {
                return double.MaxValue;
            }

            score +=
                currentScore * Scores.PlayerScoreMultiplier +
                uni.InternetLinks.Count * Scores.InternetLinkMultiplier;
            // check for production chances
            var productionChances = uni.ProductionChances;
            if (productionChances.Values.Count(v => v != 0)
                == GameConstants.RealDegrees.Length)
            {
                score += Scores.HasAllDegrees;
            }
            score += productionChances.Keys.Sum(degree =>
                                                productionChances[degree] * Scores.ProductionMultiplier *
                                                Scores.DegreeModifier[degree]);
            // evaluation special sites and normalsites
            if (uni.HasNormalTradingSite)
            {
                score += Scores.NormalSite;
            }
            score += uni.SpecialSites
                .Where(specialSite => productionChances.ContainsKey(specialSite.TradeOutDegree))
                .Sum(specialSite => productionChances[specialSite.TradeOutDegree]*Scores.SpecialSiteMultiplier);
            // take opponent's chance
            score += (from campus in uni.Campuses
                      from edge in campus.Adjacent.Edges
                      where edge.Color != uni.Color
                      select Scores.TakenOtherPlayerCampus).Sum();
            // check for free vertex
            score += uni.InternetLinks.SelectMany(l => l.Adjacent.Vertices).Distinct()
                .Where(v => v.IsFreeToBuildCampus())
                .Sum(v => game.GetVertexProductionChance(v) * Scores.FutureCampus);
            // try to maintain the lead
            if (game.MostFailedStartUps.University == uni)
            {
                if (! game.Universities.Any(other => other != uni &&
                                                     other.NumberOfFailedCompanies == uni.NumberOfFailedCompanies))
                {
                    score += Scores.LeadMostScore;
                }
            }
            if (game.LongestInternetLink.University == uni)
            {
                if (! game.Universities.Any(other => other != uni &&
                                                     other.LengthOfLongestLink == uni.LengthOfLongestLink))
                {
                    score += Scores.LeadMostScore;
                }
            }


            // check for students numbers
            double expectedStudents = uni.Students.Values.Sum();
            if (expectedStudents > GameConstants.MaxNumberOfStudents)
            {
                expectedStudents = expectedStudents*game.ProbabilityWithNoCut +
                                   (expectedStudents/2)*(1 - game.ProbabilityWithNoCut);
            }
            score += expectedStudents * Scores.StudentNumberMultiplier;

            // check for student types in hand, encourage to trade students with rare production chance
            foreach (var degree in uni.Students.Keys)
            {
                int degreeCount = uni.Students[degree];
                if (degreeCount > 0)
                {
                    double chance = productionChances[degree];
                    if (chance == 0.0)
                    {
                        chance = 0.1;
                    }
                    score += degreeCount / chance * (GameConstants.Chance.TotalDiceRoll/ 6);
                }
            }
            
            // finally
            return score;
        }
    }
}