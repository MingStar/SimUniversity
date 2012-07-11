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
        
        private static IEnumerable<IPlayerMoveForUpdate> GenerateTradeMoves(Game game)
        {
            var uni = game.CurrentUniversity;
            return uni.GetDegreeTradingRates()
                .Where(rate => uni.HasStudentsFor(rate))
                .SelectMany(GetTradingMoves);
        }

        private static IEnumerable<IPlayerMoveForUpdate> GetTradingMoves(StudentGroup studentGroup)
        {
            return (from inDegree in GameConstants.RealDegrees
                    where inDegree != studentGroup.Degree
                    select new TradingMove(studentGroup.Degree, studentGroup.Quantity, inDegree));
        }

        private static IEnumerable<IPlayerMoveForUpdate> GenerateBuildLinkMoves(Game game)
        {
            var currentUni = game.CurrentUniversity;
            if (!currentUni.HasStudentsFor(BuildLinkMove.NeededStudents))
            {
                return EmptyMoves;
            }
            return currentUni.InternetLinks.SelectMany(l => l.Adjacent.Edges).Distinct()
                .Where(e => !e.Color.HasValue)
                .Select(e => new BuildLinkMove(e.Position));
        }

        private static IEnumerable<IPlayerMoveForUpdate> GenerateBuildTradiationCampusMoves(Game game)
        {
            var currentUni = game.CurrentUniversity;
            if (!currentUni.HasStudentsFor(BuildCampusMove.StudentsNeededForTraditionalCampus))
            {
                return EmptyMoves;
            }
            return currentUni.InternetLinks.SelectMany(l => l.Adjacent.Vertices).Distinct()
                .Where(v => v.IsFreeToBuildCampus())
                .Select(v => new BuildCampusMove(v.Position, CampusType.Traditional));
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