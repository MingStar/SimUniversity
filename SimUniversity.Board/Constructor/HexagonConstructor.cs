using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                if (_hex.Hexagons[i] != null)
                    continue;
                var eo = (EdgeOrientation)i;
                var nextHex = board[_hex.GetPositionNextTo(eo)];
                if (nextHex != null)
                {
                    AtSideJoin(eo, nextHex);
                }
            }
            // create vertices
            for (int i = 0; i < BoardConstants.VertexOrentationCount; ++i)
            {
                if (_hex.Vertices[i] == null)
                {
                    var vertex = UseOtherOrCreateVertex(board, (VertexOrientation)i);
                    _hex.Vertices[i] = vertex;
                    _hex.Adjacent.Add(vertex);
                    vertex.Adjacent.Add(_hex);
                }
            }
            // create edges 
            for (int i = 0; i < BoardConstants.EdgeOrentationCount; ++i)
            {
                if (_hex.Edges[i] != null)
                    continue;
                var edge = UseOtherOrCreateEdge(board, (EdgeOrientation)i);
                _hex.Edges[i] = edge;
                _hex.Adjacent.Add(edge);
                edge.Adjacent.Add(_hex);
            }
        }



        private void AtSideJoin(EdgeOrientation eo, Hexagon hex)
        {
            _hex.AddAdjacent(hex, eo);
            hex.AddAdjacent(_hex, EdgeStaticInfo.Get(eo).OppositeEdge);
        }

        private Vertex UseOtherOrCreateVertex(Board board, VertexOrientation vo)
        {
            // can be 2 adjacent hexagons
            foreach (var pos in VertexStaticInfo.Get(vo).RelativePositions)
            {
                var hex = board[_hex.Position.Add(pos.Offset)];
                if (hex == null)
                    continue;
                var vertex = hex[pos.Orientation];
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
            var hex = board[_hex.GetPositionNextTo(eo)];
            if (hex != null)
            {
                var edge = hex[EdgeStaticInfo.Get(eo).OppositeEdge];
                if (edge != null)
                {
                    return edge;
                }
            }
            return new Edge(_hex, eo);
        }

    }
}
