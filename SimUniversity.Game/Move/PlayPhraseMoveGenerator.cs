using System;
using System.Collections.Generic;
using System.Linq;
using MingStar.SimUniversity.Board;
using MingStar.SimUniversity.Contract;

namespace MingStar.SimUniversity.Game.Move
{
    internal class PlayPhraseMoveGenerator
    {
        private static readonly List<IPlayerMove> EmptyMoves = new List<IPlayerMove>();

        public static List<IPlayerMove> GenerateAllMoves(Game game)
        {
            var possibleMoves = new List<IPlayerMove>();
            // build campuses
            possibleMoves.AddRange(GenerateBuildSuperCampusMoves(game));
            possibleMoves.AddRange(GenerateBuildTradiationCampusMoves(game));
            // trade
            possibleMoves.AddRange(GenerateTradeMoves(game));
            // build links
            possibleMoves.AddRange(GenerateBuildLinkMoves(game));
            // start up
            if (game.CurrentUniversity.HasStudentsFor(TryStartUpMove.NeededStudents))
            {
                possibleMoves.Add(new TryStartUpMove());
            }
            // finally
            possibleMoves.Add(new EndTurn());
            return possibleMoves;
        }

        public static List<Type> GenerateAllMoveTypes(Game game)
        {
            return null;
        }

        private static IEnumerable<IPlayerMove> GenerateTradeMoves(Game game)
        {
            University currentUni = game.CurrentUniversity;
            var moves = new List<IPlayerMove>();
            var checkedD = new HashSet<DegreeType>();
            foreach (SpecialTradingSite specialSite in currentUni.SpecialSites)
            {
                if (currentUni.HasStudentsFor(specialSite.StudentsNeeded))
                {
                    checkedD.Add(specialSite.TradeOutDegree);
                    AddTradingMoves(moves, specialSite.TradeOutDegree, SpecialTradingSite.TradeOutStudentQuantity);
                }
            }
            foreach (DegreeType outDegree in Constants.RealDegrees)
            {
                if (checkedD.Contains(outDegree))
                {
                    continue;
                }
                if (currentUni.HasNormalTradingSite
                    && currentUni.HasStudentsFor(outDegree, TradingSite.TradeOutStudentQuantity))
                {
                    AddTradingMoves(moves, outDegree, TradingSite.TradeOutStudentQuantity);
                }
                else if (currentUni.HasStudentsFor(outDegree, GameConstants.NormalTradingStudentQuantity))
                {
                    AddTradingMoves(moves, outDegree, GameConstants.NormalTradingStudentQuantity);
                }
            }
            return moves;
        }

        private static void AddTradingMoves(List<IPlayerMove> moves, DegreeType outDegree, int quantity)
        {
            foreach (DegreeType degree in Constants.RealDegrees)
            {
                if (degree == outDegree)
                {
                    continue;
                }
                moves.Add(new TradingMove(outDegree, quantity, degree));
            }
        }

        private static List<IPlayerMove> GenerateBuildLinkMoves(Game game)
        {
            University currentUni = game.CurrentUniversity;
            if (!currentUni.HasStudentsFor(BuildLinkMove.NeededStudents))
            {
                return EmptyMoves;
            }
            var checkedE = new HashSet<Edge>();
            var moves = new List<IPlayerMove>();
            foreach (Edge link in currentUni.InternetLinks)
            {
                foreach (Edge edge in link.Adjacent.Edges)
                {
                    if (edge.Color == null &&
                        !checkedE.Contains(edge))
                    {
                        // we can build link
                        moves.Add(new BuildLinkMove(edge.Position));
                    }
                    checkedE.Add(edge);
                }
            }
            return moves;
        }

        private static List<IPlayerMove> GenerateBuildTradiationCampusMoves(Game game)
        {
            University currentUni = game.CurrentUniversity;
            var checkedV = new HashSet<Vertex>();
            var moves = new List<IPlayerMove>();
            if (currentUni.HasStudentsFor(BuildCampusMove.StudentsNeededForTraditionalCampus))
            {
                foreach (Edge link in currentUni.InternetLinks)
                {
                    foreach (Vertex vertex in link.Adjacent.Vertices)
                    {
                        if (!checkedV.Contains(vertex)
                            && vertex.IsFreeToBuildCampus())
                        {
                            // we can build on this vertex
                            moves.Add(new BuildCampusMove(vertex.Position, CampusType.Traditional));
                        }
                        checkedV.Add(vertex);
                    }
                }
            }
            return moves;
        }

        private static List<IPlayerMove> GenerateBuildSuperCampusMoves(Game game)
        {
            University currentUni = game.CurrentUniversity;
            if (currentUni.HasStudentsFor(BuildCampusMove.StudentsNeededForSuperCampus))
            {
                return (from campus in currentUni.Campuses
                        select (IPlayerMove) (new BuildCampusMove(campus.Position, CampusType.Super))
                       ).ToList();
            }
            else
            {
                return EmptyMoves;
            }
        }
    }
}