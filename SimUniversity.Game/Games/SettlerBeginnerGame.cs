using MingStar.SimUniversity.Board.Constructor;
using MingStar.SimUniversity.Contract;
using MingStar.SimUniversity.Game.Move;

namespace MingStar.SimUniversity.Game.Games
{
    public class SettlerBeginnerGame : Game
    {
        public SettlerBeginnerGame() : base((new SettlerBeginnerBoardConstructor()).ConstructBoard(), 4)
        {
            Setup();
        }

        private void Setup()
        {
            // red
            ApplyMove(new BuildCampusMove(new VertexPosition(0, 3, VertexOrientation.BottomLeft), CampusType.Traditional));
            ApplyMove(new BuildLinkMove(new EdgePosition(0, 3, EdgeOrientation.BottomLeft)));

            //blue
            ApplyMove(new BuildCampusMove(new VertexPosition(1, 0, VertexOrientation.Right), CampusType.Traditional));
            ApplyMove(new BuildLinkMove(new EdgePosition(1, 0, EdgeOrientation.BottomRight)));

            //white
            ApplyMove(new BuildCampusMove(new VertexPosition(-1, 3, VertexOrientation.Left), CampusType.Traditional));
            ApplyMove(new BuildLinkMove(new EdgePosition(-1, 3, EdgeOrientation.TopLeft)));

            //orange
            ApplyMove(new BuildCampusMove(new VertexPosition(1, 3, VertexOrientation.TopLeft), CampusType.Traditional));
            ApplyMove(new BuildLinkMove(new EdgePosition(1, 3, EdgeOrientation.TopLeft)));

            //orange
            ApplyMove(new BuildCampusMove(new VertexPosition(2, 2, VertexOrientation.Left), CampusType.Traditional));
            ApplyMove(new BuildLinkMove(new EdgePosition(2, 2, EdgeOrientation.BottomLeft)));

            //white
            ApplyMove(new BuildCampusMove(new VertexPosition(2, 0, VertexOrientation.TopLeft), CampusType.Traditional));
            ApplyMove(new BuildLinkMove(new EdgePosition(2, 0, EdgeOrientation.TopLeft)));

            //blue
            ApplyMove(new BuildCampusMove(new VertexPosition(0, 0, VertexOrientation.TopLeft), CampusType.Traditional));
            ApplyMove(new BuildLinkMove(new EdgePosition(0, 1, EdgeOrientation.BottomLeft)));

            //red
            ApplyMove(new BuildCampusMove(new VertexPosition(0, 2, VertexOrientation.Right), CampusType.Traditional));
            ApplyMove(new BuildLinkMove(new EdgePosition(0, 2, EdgeOrientation.TopRight)));
        }
    }
}