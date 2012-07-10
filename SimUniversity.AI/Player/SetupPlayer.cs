using System.Collections.Generic;
using System.Linq;
using MingStar.SimUniversity.AI.Evaluation;
using MingStar.SimUniversity.Contract;
using MingStar.SimUniversity.Game;
using MingStar.SimUniversity.Game.Move;
using MingStar.Utilities.Linq;

namespace MingStar.SimUniversity.AI.Player
{
    public class SetupPlayer : IPlayer
    {
        private readonly GameEvaluation _gameEvaluation;

        public SetupPlayer(GameEvaluation gameEvaluation)
        {
            _gameEvaluation = gameEvaluation;
        }

        #region IPlayer Members

        public List<IPlayerMove> MakeMoves(IGame game)
        {
            IEnumerable<IPlayerMove> allMoves = game.GenerateAllMoves();
            IEnumerable<ScoredMove> result = (from move in allMoves.Shuffle()
                                              select ScoreMove(game, move));
            IOrderedEnumerable<ScoredMove> result2 = (from scoredMove in result
                                                      orderby scoredMove.Score descending
                                                      select scoredMove);
            return new List<IPlayerMove> {result2.First().Move};
        }

        public string Name
        {
            get { return "Setup"; }
        }

        #endregion

        private ScoredMove ScoreMove(IGame igame, IPlayerMove move)
        {
            var game = (Game.Game) igame;
            var buildLinkMove = move as BuildLinkMove;
            if (buildLinkMove != null)
            {
                return GetBuildLinkMoveScore(game, buildLinkMove);
            }
            var buildCampus = move as BuildCampusMove;
            return buildCampus == null
                       ? new ScoredMove(move, 100)
                       : new ScoredMove(move, GetVertexScore(game, game.IBoard[buildCampus.WhereAt]));
        }

        private double GetVertexScore(IGame game, IVertex vertex)
        {
            if (!vertex.IsFreeToBuildCampus())
            {
                return 0.0;
            }
            // current production chance for uni
            var productionChances = new DegreeCount {game.CurrentIUniversity.ProductionChances};
            foreach (IHexagon hex in vertex.Adjacent.Hexagons)
            {
                productionChances[hex.Degree] += GameConstants.HexID2Chance[hex.ProductionNumber];
            }
            double score = 0.0;
            if (game.CurrentPhase == GamePhase.Setup2)
            {
                int hasDegreeNumber = productionChances.Values.Count(v => v != 0);
                if (hasDegreeNumber == Constants.RealDegrees.Length)
                {
                    score += _gameEvaluation.Scores.ProductionBase*3;
                }
                else if (hasDegreeNumber == Constants.RealDegrees.Length - 1)
                {
                    score += _gameEvaluation.Scores.ProductionBase;
                }
            }
            var productionScores = new Dictionary<DegreeType, double>();
            foreach (DegreeType degree in productionChances.Keys)
            {
                productionScores[degree] =
                    productionChances[degree]*_gameEvaluation.Scores.ProductionBase;
            }
            if (game.CurrentPhase == GamePhase.Setup1)
            {
                // amplify the production scores for a certain degree
                foreach (DegreeType degree in productionChances.Keys)
                {
                    productionScores[degree] *= game.Scarcity[degree]*_gameEvaluation.Scores.SetupDegreeModifier[degree];
                }
            }
            else // Setup2
            {
                foreach (DegreeType degree in productionChances.Keys)
                {
                    productionScores[degree] *= _gameEvaluation.Scores.DegreeModifier[degree];
                }
                // site score
                if (vertex.TradingSite != null)
                {
                    var specialSite = vertex.TradingSite as ISpecialTradingSite;
                    if (specialSite != null)
                    {
                        score += productionChances[specialSite.TradeOutDegree]*
                                 _gameEvaluation.Scores.SpecialSiteMultiplier;
                    }
                    else // normal site
                    {
                        score += _gameEvaluation.Scores.NormalSite;
                    }
                }
            }
            score += productionScores.Values.Sum();
            return score;
        }


        private ScoredMove GetBuildLinkMoveScore(IGame game, BuildLinkMove buildLinkMove)
        {
            int remainingSetupTurn = game.NumberOfUniversities*GameConstants.NumberOfInitialSetups - game.CurrentTurn;
            if (remainingSetupTurn < 0)
            {
                remainingSetupTurn = 0;
            }
            var vertexScores = new Dictionary<IVertex, double>();
            foreach (IVertex vertex in game.IBoard.GetVertices())
            {
                vertexScores[vertex] = GetVertexScore(game, vertex);
            }
            IOrderedEnumerable<KeyValuePair<IVertex, double>> result = from pair in vertexScores
                                                                       orderby pair.Value descending
                                                                       select pair;
            var adjustedVertexScores = new Dictionary<IVertex, double>();
            foreach (var pair in result.Skip(remainingSetupTurn))
            {
                adjustedVertexScores[pair.Key] = pair.Value;
            }
            var checkedEdges = new HashSet<IEdge>();
            IEdge edge = game.IBoard[buildLinkMove.WhereAt];

            double score = GetEdgeScore(edge, checkedEdges, adjustedVertexScores, 1);
            return new ScoredMove(buildLinkMove, score);
        }

        private double GetEdgeScore(IEdge edge, HashSet<IEdge> checkedEdges,
                                    Dictionary<IVertex, double> vertexScores, int level)
        {
            if (checkedEdges.Contains(edge))
            {
                return 0.0;
            }
            checkedEdges.Add(edge);
            double score = 0.0;
            // get adjacent vertex scores
            foreach (IVertex vertex in edge.Adjacent.Vertices)
            {
                if (vertexScores.ContainsKey(vertex))
                {
                    score += vertexScores[vertex];
                    vertexScores.Remove(vertex);
                }
            }
            score /= level;
            // other edge scores
            foreach (IEdge nextEdge in edge.Adjacent.Edges)
            {
                if (nextEdge.Color == null)
                {
                    score += GetEdgeScore(nextEdge, checkedEdges, vertexScores, level + 1);
                }
            }
            return score;
        }
    }
}