using System.Collections.Generic;
using System.Linq;
using MingStar.SimUniversity.Board;
using MingStar.SimUniversity.Contract;
using MingStar.SimUniversity.Game;

namespace MingStar.SimUniversity.AI.Evaluation
{
    public class GameEvaluation
    {
        public GameEvaluation(GameScores scores)
        {
            Scores = scores;
        }

        public GameScores Scores { get; internal set; }

        public double Evaluate(Game.Game game, University uni)
        {
            double score = 0.0;
            int currentScore = game.GetScore(uni);

            if (currentScore >= game.Rules.WinningScore)
            {
                return double.MaxValue;
            }

            score +=
                currentScore*Scores.PLAYER_SCORE_BASE +
                uni.InternetLinks.Count*Scores.INTERNET_LINK_MULTIPLIER;
            // check for production chances
            DegreeCount productionChances = uni.ProductionChances;
            if (productionChances.Values.Count(v => v != 0)
                == Constants.RealDegrees.Length)
            {
                score += Scores.HAS_ALL_DEGREES;
            }
            foreach (DegreeType degree in productionChances.Keys)
            {
                score += productionChances[degree]*Scores.PRODUCTION_BASE*Scores.DegreeModifier[degree];
            }
            // evaluation special sites and normalsites
            if (uni.HasNormalTradingSite)
            {
                score += Scores.NORMAL_SITE;
            }
            foreach (SpecialTradingSite specialSite in uni.SpecialSites)
            {
                if (productionChances.ContainsKey(specialSite.TradeOutDegree))
                {
                    score += productionChances[specialSite.TradeOutDegree]*Scores.SPECIAL_SITE_MULTIPLIER;
                }
            }
            // take opponent's chance
            foreach (Vertex campus in uni.Campuses)
            {
                foreach (Edge edge in campus.Adjacent.Edges)
                {
                    if (edge.Color != uni.Color)
                    {
                        score += Scores.TAKEN_OTHER_PLAYER_CAMPUS;
                    }
                }
            }
            // check for free vertex
            var checkedV = new HashSet<Vertex>();
            foreach (Edge link in uni.InternetLinks)
            {
                foreach (Vertex vertex in link.Adjacent.Vertices)
                {
                    if (!checkedV.Contains(vertex) && vertex.IsFreeToBuildCampus())
                    {
                        score += game.GetVertexProductionChance(vertex)*Scores.FUTURE_CAMPUS;
                    }
                    checkedV.Add(vertex);
                }
            }
            // try to maintain the lead
            if (game.MostFailedStartUps.University == uni)
            {
                if (! game.Universities.Any(other => other != uni &&
                                                     other.NumberOfFailedCompanies == uni.NumberOfFailedCompanies))
                {
                    score += Scores.LEAD_MOST_SCORE;
                }
            }
            if (game.LongestInternetLink.University == uni)
            {
                if (! game.Universities.Any(other => other != uni &&
                                                     other.LengthOfLongestLink == uni.LengthOfLongestLink))
                {
                    score += Scores.LEAD_MOST_SCORE;
                }
            }


            // check for students numbers
            double expectedStudents = uni.Students.Values.Sum();
            if (expectedStudents > GameConstants.MaxNumberOfStudents)
            {
                expectedStudents = expectedStudents*game.ProbabilityWithNoCut +
                                   (expectedStudents/2)*(1 - game.ProbabilityWithNoCut);
            }
            score += expectedStudents*Scores.STUDENT_NUMBER;

            // check for student types
            foreach (DegreeType degree in uni.Students.Keys)
            {
                int degreeCount = uni.Students[degree];
                if (degreeCount > 0)
                {
                    double chance = productionChances[degree];
                    if (chance == 0.0)
                    {
                        chance = 0.1;
                    }
                    score += degreeCount/chance*(GameConstants.Chance.TotalDiceRoll/6);
                }
            }
            return score;
        }
    }
}