using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using MingStar.SimUniversity.AI.Evaluation;
using MingStar.SimUniversity.Board;
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
            ReadOnlyCollection<IPlayerMove> allMoves = game.GenerateAllMoves();
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
            if (move is BuildLinkMove)
            {
                return GetBuildLinkMoveScore(game, (BuildLinkMove) move);
            }

            var buildCampus = move as BuildCampusMove;
            if (buildCampus == null)
            {
                return new ScoredMove(move, 100);
            }
            return new ScoredMove(move, GetVertexScore(game, game.Board[buildCampus.WhereAt]));
        }

        private double GetVertexScore(Game.Game game, Vertex vertex)
        {
            if (!vertex.IsFreeToBuildCampus())
            {
                return 0.0;
            }
            // current production chance for uni
            var productionChances = new DegreeCount();
            productionChances.Add(game.CurrentUniversity.ProductionChances);
            foreach (Hexagon hex in vertex.Adjacent.Hexagons)
            {
                productionChances[hex.Degree] += GameConstants.HexID2Chance[hex.ID];
            }
            double score = 0.0;
            if (game.CurrentPhase == GamePhase.Setup2)
            {
                int hasDegreeNumber = productionChances.Values.Count(v => v != 0);
                if (hasDegreeNumber ==Constants.RealDegrees.Length)
                {
                    score += _gameEvaluation.Scores.PRODUCTION_BASE*3;
                }
                else if (hasDegreeNumber == Constants.RealDegrees.Length - 1)
                {
                    score += _gameEvaluation.Scores.PRODUCTION_BASE;
                }
            }
            var productionScores = new Dictionary<DegreeType, double>();
            foreach (DegreeType degree in productionChances.Keys)
            {
                productionScores[degree] =
                    productionChances[degree]*_gameEvaluation.Scores.PRODUCTION_BASE;
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
                if (vertex.TradingSite == TradingSite.Instance)
                {
                    score += _gameEvaluation.Scores.NORMAL_SITE;
                }
                else if (vertex.TradingSite != null)
                {
                    var specialSite = vertex.TradingSite as SpecialTradingSite;
                    if (specialSite != null)
                    {
                        score += productionChances[specialSite.TradeOutDegree]*
                                 _gameEvaluation.Scores.SPECIAL_SITE_MULTIPLIER;
                    }
                }
            }
            score += productionScores.Values.Sum();
            return score;
        }


        private ScoredMove GetBuildLinkMoveScore(Game.Game game, BuildLinkMove buildLinkMove)
        {
            int remainingSetupTurn = game.NumberOfUniversities*GameConstants.NumberOfInitialSetups - game.CurrentTurn;
            if (remainingSetupTurn < 0)
            {
                remainingSetupTurn = 0;
            }
            var vertexScores = new Dictionary<Vertex, double>();
            foreach (Vertex vertex in game.Board.GetVertices())
            {
                vertexScores[vertex] = GetVertexScore(game, vertex);
            }
            IOrderedEnumerable<KeyValuePair<Vertex, double>> result = from pair in vertexScores
                                                                      orderby pair.Value descending
                                                                      select pair;
            var adjustedVertexScores = new Dictionary<Vertex, double>();
            foreach (var pair in result.Skip(remainingSetupTurn))
            {
                adjustedVertexScores[pair.Key] = pair.Value;
            }
            var checkedEdges = new HashSet<Edge>();
            Edge edge = game.Board[buildLinkMove.WhereAt];

            double score = GetEdgeScore(game, edge, checkedEdges, adjustedVertexScores, 1);
            return new ScoredMove(buildLinkMove, score);
        }

        private double GetEdgeScore(Game.Game game, Edge edge, HashSet<Edge> checkedEdges,
                                    Dictionary<Vertex, double> vertexScores, int level)
        {
            if (checkedEdges.Contains(edge))
            {
                return 0.0;
            }
            checkedEdges.Add(edge);
            double score = 0.0;
            // get adjacent vertex scores
            foreach (Vertex vertex in edge.Adjacent.Vertices)
            {
                if (vertexScores.ContainsKey(vertex))
                {
                    score += vertexScores[vertex];
                    vertexScores.Remove(vertex);
                }
            }
            score /= level;
            // other edge scores
            foreach (Edge nextEdge in edge.Adjacent.Edges)
            {
                if (nextEdge.Color == null)
                {
                    score += GetEdgeScore(game, nextEdge, checkedEdges, vertexScores, level + 1);
                }
            }
            return score;
        }
    }
}