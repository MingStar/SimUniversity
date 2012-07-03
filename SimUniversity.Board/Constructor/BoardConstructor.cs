using System.Linq;
using MingStar.SimUniversity.Contract;

namespace MingStar.SimUniversity.Board.Constructor
{
    public class BoardConstructor
    {
        private Hexagon _lastPlacedHexagon;
        public Board Board { get; private set; }

        public BoardConstructor()
        {
            Board = new Board();
        }

        protected void PlaceFirstHexagon(int id, DegreeType degreeType)
        {
            Hexagon hex = Board.CreateHexagon(id, degreeType, new Position(0, 0));
            _lastPlacedHexagon = hex;
        }

        protected void PlaceNextHexagon(int id, DegreeType degreeType, EdgeOrientation eo)
        {
            var pos = _lastPlacedHexagon.GetPositionNextTo(eo);
            Board.GetLimits(pos);
            var hex = Board.CreateHexagon(id, degreeType, pos);
            _lastPlacedHexagon = hex;
        }

        protected void PlaceHexagonsEnd()
        {
            _lastPlacedHexagon = null;
            foreach (var hex in Board.GetHexagons())
            {
                (new HexagonConstructor(hex)).PlaceEnd(Board);
            }
        }

        public void SetNormalSites(int x, int y, VertexOrientation vo, VertexOrientation vo2)
        {
            var hex = Board[x, y];
            if (hex == null)
                return;
            hex[vo].MakeMultiSite();
            hex[vo2].MakeMultiSite();
        }

        public void SetSpecializedSites(int x, int y,
                                        VertexOrientation vo, VertexOrientation vo2, DegreeType degree)
        {
            Hexagon hex = Board[x, y];
            if (hex != null)
            {
                hex[vo].MakeSpecialSite(degree);
                hex[vo2].MakeSpecialSite(degree);
            }
        }

        protected void Lock()
        {
            Board.IsLocked = true;
            FindAllAdjacents();
        }

        private void FindAllAdjacents()
        {
            // for all vertices
            foreach (var vertex in Board.GetVertices())
            {
                // for all adjacent hexagons
                foreach (var hex in vertex.Adjacent.Hexagons)
                {
                    // for all vertices on the hexagon
                    for (int i = 0; i < BoardConstants.VertexOrentationCount; ++i)
                    {
                        var thisOritentation = (VertexOrientation) i;
                        if (hex[thisOritentation] == vertex)
                        {
                            var staticInfo = VertexStaticInfo.Get(thisOritentation);
                            // add edges
                            vertex.Adjacent.Add(
                                staticInfo.AdjacentEdgeOrientations.Select(adjEo => hex[adjEo])
                                );
                            // add vertices
                            vertex.Adjacent.Add(
                                staticInfo.AdjacentVertexOrientations.Select(adjVo => hex[adjVo])
                                );
                        }
                    }
                }
            }
            var allEdges = Board.GetEdges();
            foreach (var edge in allEdges)
            {
                edge.FindAllAdjacents(Board);
            }
            foreach (var edge in allEdges)
            {
                edge.Cache();
            }
        }
    }
}
