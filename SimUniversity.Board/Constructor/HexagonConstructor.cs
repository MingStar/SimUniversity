using MingStar.SimUniversity.Board.Positioning;
using MingStar.SimUniversity.Contract;

namespace MingStar.SimUniversity.Board.Constructor
{
    public class HexagonConstructor
    {
        private readonly Hexagon _hex;

        public HexagonConstructor(Hexagon hex)
        {
            _hex = hex;
        }

        internal void PlaceEnd(Board board)
        {
            // set adjacent hexagons
            for (int i = 0; i < BoardConstants.EdgeOrentationCount; ++i)
            {
                var eo = (EdgeOrientation) i;
                if (_hex[eo] != null)
                    continue;
                Hexagon nextHex = board[_hex.GetPositionNextTo(eo)];
                if (nextHex != null)
                {
                    AtSideJoin(eo, nextHex);
                }
            }
            // create vertices
            for (int i = 0; i < BoardConstants.VertexOrentationCount; ++i)
            {
                var vo = (VertexOrientation) i;
                if (_hex[vo] != null)
                {
                    continue;
                }
                Vertex vertex = UseOtherOrCreateVertex(board, vo);
                _hex[vo] = vertex;
                _hex.AdjacentForUpdate.Add(vertex);
                vertex.AdjacentForUpdate.Add(_hex);
            }
            // create edges 
            for (int i = 0; i < BoardConstants.EdgeOrentationCount; ++i)
            {
                var eo = (EdgeOrientation) i;
                if (_hex[eo] != null)
                {
                    continue;
                }
                Edge edge = UseOtherOrCreateEdge(board, eo);
                _hex[eo] = edge;
                _hex.AdjacentForUpdate.Add(edge);
                edge.AdjacentForUpdate.Add(_hex);
            }
        }

        internal void AddAdjacent(Hexagon hex, EdgeOrientation eo, Hexagon otherHex)
        {
            hex.AdjacentForUpdate.Add(otherHex);
        }

        private void AtSideJoin(EdgeOrientation eo, Hexagon hex)
        {
            AddAdjacent(_hex, eo, hex);
            AddAdjacent(hex, EdgeStaticInfo.Get(eo).OppositeEdge, _hex);
        }

        private Vertex UseOtherOrCreateVertex(Board board, VertexOrientation vo)
        {
            // can be 2 adjacent hexagons
            foreach (VertexStaticInfo.RelativePosition pos in VertexStaticInfo.Get(vo).RelativePositions)
            {
                Hexagon hex = board[_hex.Position.Add(pos.Offset)];
                if (hex == null)
                    continue;
                Vertex vertex = hex[pos.Orientation];
                if (vertex != null)
                {
                    return vertex;
                }
            }
            return new Vertex(_hex, vo);
        }

        private Edge UseOtherOrCreateEdge(Board board, EdgeOrientation eo)
        {
            // only one adjacent hexagon
            Hexagon hex = board[_hex.GetPositionNextTo(eo)];
            if (hex != null)
            {
                Edge edge = hex[EdgeStaticInfo.Get(eo).OppositeEdge];
                if (edge != null)
                {
                    return edge;
                }
            }
            return new Edge(_hex, eo);
        }
    }
}