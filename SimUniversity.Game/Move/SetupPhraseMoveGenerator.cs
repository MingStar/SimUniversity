using System.Collections.Generic;
using System.Linq;
using MingStar.SimUniversity.Contract;

namespace MingStar.SimUniversity.Game.Move
{
    public class SetupPhraseMoveGenerator
    {
        public SetupPhraseMoveGenerator()
        {
            ShouldBuildCampus = true;
        }

        public bool ShouldBuildCampus { get; private set; }

        public void Toggle()
        {
            ShouldBuildCampus = !ShouldBuildCampus;
        }

        public List<IPlayerMoveForUpdate> GenerateAllMoves(Game game)
        {
            return (ShouldBuildCampus
                        ? GenerateBuildTradiationCampusMoves(game)
                        : GenerateBuildLinkMoves(game)).ToList();
        }

        private static IEnumerable<IPlayerMoveForUpdate> GenerateBuildLinkMoves(Game game)
        {
            University currentUni = game.CurrentUniversity;
            return (from campus in currentUni.Campuses
                    where campus.Adjacent.Edges.All(e => e.Color == null)
                    from edge in campus.Adjacent.Edges
                    select new BuildLinkMove(edge.Position));
        }

        private static IEnumerable<IPlayerMoveForUpdate> GenerateBuildTradiationCampusMoves(Game game)
        {
            return (from vertex in game.Board.GetVertices()
                    where vertex.IsFreeToBuildCampus()
                    select new BuildCampusMove(vertex.Position, CampusType.Traditional));
        }
    }
}