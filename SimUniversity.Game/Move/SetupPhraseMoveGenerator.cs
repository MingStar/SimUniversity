using System.Collections.Generic;
using System.Linq;
using MingStar.SimUniversity.Board;
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

        public List<IPlayerMove> GenerateAllMoves(Game game)
        {
            var possibleMoves = new List<IPlayerMove>();
            if (ShouldBuildCampus)
            {
                // build campuses
                possibleMoves.AddRange(GenerateBuildTradiationCampusMoves(game));
            }
            else
            {
                // build links
                possibleMoves.AddRange(GenerateBuildLinkMoves(game));
            }
            return possibleMoves;
        }

        private static List<IPlayerMove> GenerateBuildLinkMoves(Game game)
        {
            University currentUni = game.CurrentUniversity;
            var moves = new List<IPlayerMove>();
            foreach (Vertex campus in currentUni.Campuses)
            {
                if (campus.Adjacent.Edges.Any(e => e.Color != null))
                {
                    continue;
                }
                foreach (Edge edge in campus.Adjacent.Edges)
                {
                    // we can build link
                    moves.Add(new BuildLinkMove(edge.Position));
                }
            }
            return moves;
        }

        private static List<IPlayerMove> GenerateBuildTradiationCampusMoves(Game game)
        {
            University currentUni = game.CurrentUniversity;
            var checkedV = new HashSet<Vertex>();
            var moves = new List<IPlayerMove>();
            foreach (Vertex vertex in game.Board.GetVertices())
            {
                if (vertex.IsFreeToBuildCampus())
                {
                    moves.Add(new BuildCampusMove(vertex.Position, CampusType.Traditional));
                }
            }
            return moves;
        }
    }
}