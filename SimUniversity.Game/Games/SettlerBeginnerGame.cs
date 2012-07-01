using MingStar.SimUniversity.Board.Constructor;
using MingStar.SimUniversity.Contract;

namespace MingStar.SimUniversity.Game.Games
{
    public class SettlerBeginnerGame : Game
    {
        public SettlerBeginnerGame() : base((new SettlerBoardConstructor()).Board, 4)
        {
            Setup();
        }

        //public override void Reset()
        //{
        //    base.Reset();
        //    Setup();
        //}

        private void Setup()
        {
            // red
            BuildCampus(0, 3, VertexOrientation.BottomLeft, CampusType.Traditional);
            SetUpLink(0, 3, EdgeOrientation.BottomLeft);
            NextTurn();

            //blue
            BuildCampus(0, 0, VertexOrientation.TopLeft, CampusType.Traditional);
            SetUpLink(0, 1, EdgeOrientation.BottomLeft);
            NextTurn();

            //white
            BuildCampus(-1, 3, VertexOrientation.Left, CampusType.Traditional);
            //BuildLink(-2, 3, EdgeOrientation.Top);
            SetUpLink(-1, 3, EdgeOrientation.TopLeft);
            NextTurn();

            //orange
            BuildCampus(2, 2, VertexOrientation.Left, CampusType.Traditional);
            SetUpLink(2, 2, EdgeOrientation.BottomLeft);
            NextTurn();

            //orange
            BuildCampus(1, 3, VertexOrientation.TopLeft, CampusType.Traditional);
            SetUpLink(1, 3, EdgeOrientation.TopLeft);
            NextTurn();

            //white
            BuildCampus(2, 0, VertexOrientation.TopLeft, CampusType.Traditional);
            SetUpLink(2, 0, EdgeOrientation.TopLeft);
            NextTurn();

            //blue
            BuildCampus(1, 0, VertexOrientation.Right, CampusType.Traditional);
            SetUpLink(1, 0, EdgeOrientation.BottomRight);
            NextTurn();

            //red
            BuildCampus(0, 2, VertexOrientation.Right, CampusType.Traditional);
            SetUpLink(0, 2, EdgeOrientation.TopRight);
            NextTurn();
        }
    }
}