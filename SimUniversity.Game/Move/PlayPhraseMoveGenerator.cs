using System;
using System.Collections.Generic;
using System.Linq;
using MingStar.SimUniversity.Board;
using MingStar.SimUniversity.Contract;

namespace MingStar.SimUniversity.Game.Move
{
    internal class PlayPhraseMoveGenerator
    {
        private static readonly List<IPlayerMoveForUpdate> EmptyMoves = new List<IPlayerMoveForUpdate>();

        public static List<IPlayerMoveForUpdate> GenerateAllMoves(Game game)
        {
            var possibleMoves = new List<IPlayerMoveForUpdate>();
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

        private static IEnumerable<IPlayerMoveForUpdate> GenerateTradeMoves(Game game)
        {
            University currentUni = game.CurrentUniversity;
            var moves = new List<IPlayerMoveForUpdate>();
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
                    && currentUni.HasStudentsFor(new StudentGroup(outDegree, TradingSite.TradeOutStudentQuantity)))
                {
                    AddTradingMoves(moves, outDegree, TradingSite.TradeOutStudentQuantity);
                }
                else if (
                    currentUni.HasStudentsFor(new StudentGroup(outDegree, GameConstants.NormalTradingStudentQuantity)))
                {
                    AddTradingMoves(moves, outDegree, GameConstants.NormalTradingStudentQuantity);
                }
            }
            return moves;
        }

        private static void AddTradingMoves(List<IPlayerMoveForUpdate> moves, DegreeType outDegree, int quantity)
        {
            moves.AddRange((from degree in Constants.RealDegrees
                            where degree != outDegree
                            select new TradingMove(outDegree, quantity, degree)));
        }

        private static IEnumerable<IPlayerMoveForUpdate> GenerateBuildLinkMoves(Game game)
        {
            University currentUni = game.CurrentUniversity;
            if (!currentUni.HasStudentsFor(BuildLinkMove.NeededStudents))
            {
                return EmptyMoves;
            }
            var checkedE = new HashSet<IEdge>();
            var moves = new List<IPlayerMoveForUpdate>();
            foreach (IEdge link in currentUni.InternetLinks)
            {
                foreach (IEdge edge in link.Adjacent.Edges)
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

        private static IEnumerable<IPlayerMoveForUpdate> GenerateBuildTradiationCampusMoves(Game game)
        {
            University currentUni = game.CurrentUniversity;
            var checkedV = new HashSet<IVertex>();
            var moves = new List<IPlayerMoveForUpdate>();
            if (currentUni.HasStudentsFor(BuildCampusMove.StudentsNeededForTraditionalCampus))
            {
                foreach (IEdge link in currentUni.InternetLinks)
                {
                    foreach (IVertex vertex in link.Adjacent.Vertices)
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

        private static IEnumerable<IPlayerMoveForUpdate> GenerateBuildSuperCampusMoves(Game game)
        {
            University currentUni = game.CurrentUniversity;
            if (currentUni.HasStudentsFor(BuildCampusMove.StudentsNeededForSuperCampus))
            {
                return (from campus in currentUni.Campuses
                        select new BuildCampusMove(campus.Position, CampusType.Super)
                       );
            }
            return EmptyMoves;
        }
    }
}