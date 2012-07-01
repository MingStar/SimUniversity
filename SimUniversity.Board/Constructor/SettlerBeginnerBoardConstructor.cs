using MingStar.SimUniversity.Contract;

namespace MingStar.SimUniversity.Board.Constructor
{
    public class SettlerBeginnerBoardConstructor : BoardConstructor
    {
        public SettlerBeginnerBoardConstructor()
        {
            // 1st bottom row - left to right
            PlaceFirstHexagon(5, DegreeType.Ore);
            PlaceNextHexagon(2, DegreeType.Grain, EdgeOrientation.TopRight);
            PlaceNextHexagon(6, DegreeType.Wood, EdgeOrientation.TopRight);
            // 2nd row - right to left
            PlaceNextHexagon(3, DegreeType.Ore, EdgeOrientation.Top);
            PlaceNextHexagon(9, DegreeType.Sheep, EdgeOrientation.BottomLeft);
            PlaceNextHexagon(10, DegreeType.Sheep, EdgeOrientation.BottomLeft);
            PlaceNextHexagon(8, DegreeType.Brick, EdgeOrientation.BottomLeft);
            // 3rd row - left to right
            PlaceNextHexagon(0, DegreeType.Brick, EdgeOrientation.TopLeft);
            PlaceNextHexagon(3, DegreeType.Wood, EdgeOrientation.TopRight);
            PlaceNextHexagon(11, DegreeType.Grain, EdgeOrientation.TopRight);
            PlaceNextHexagon(4, DegreeType.Wood, EdgeOrientation.TopRight);
            PlaceNextHexagon(8, DegreeType.Grain, EdgeOrientation.TopRight);
            // 4th row - right to left
            PlaceNextHexagon(10, DegreeType.Sheep, EdgeOrientation.TopLeft);
            PlaceNextHexagon(5, DegreeType.Brick, EdgeOrientation.BottomLeft);
            PlaceNextHexagon(6, DegreeType.Ore, EdgeOrientation.BottomLeft);
            PlaceNextHexagon(4, DegreeType.Brick, EdgeOrientation.BottomLeft);
            // 5th row - left to right
            PlaceNextHexagon(11, DegreeType.Wood, EdgeOrientation.Top);
            PlaceNextHexagon(12, DegreeType.Sheep, EdgeOrientation.TopRight);
            PlaceNextHexagon(9, DegreeType.Grain, EdgeOrientation.TopRight);
            // end
            PlaceHexagonsEnd();
            // set sites
            SetNormalSites(0, 0, VertexOrientation.BottomLeft, VertexOrientation.BottomRight);
            SetSpecializedSites(-1, 1, VertexOrientation.Left, VertexOrientation.BottomLeft, DegreeType.Grain);
            SetSpecializedSites(-2, 3, VertexOrientation.Left, VertexOrientation.BottomLeft, DegreeType.Ore);
            SetNormalSites(-2, 4, VertexOrientation.TopLeft, VertexOrientation.Left);
            SetSpecializedSites(-1, 4, VertexOrientation.TopLeft, VertexOrientation.TopRight, DegreeType.Sheep);
            SetNormalSites(1, 3, VertexOrientation.TopLeft, VertexOrientation.TopRight);
            SetNormalSites(2, 2, VertexOrientation.TopRight, VertexOrientation.Right);
            SetSpecializedSites(2, 1, VertexOrientation.Right, VertexOrientation.BottomRight, DegreeType.Brick);
            SetSpecializedSites(1, 0, VertexOrientation.Right, VertexOrientation.BottomRight, DegreeType.Wood);
            Lock();
        }
    }
}