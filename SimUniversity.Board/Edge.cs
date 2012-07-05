using System.Collections.Generic;
using System.Linq;
using MingStar.SimUniversity.Board.Cache;
using MingStar.SimUniversity.Contract;

namespace MingStar.SimUniversity.Board
{
    public class Edge : Place
    {
        public readonly EdgePosition Position;
        private readonly EdgeCache _cache;

        private readonly Hexagon _originalHexagon;
        private readonly EdgeOrientation _originalOrientation;

        public Edge(Hexagon hex, EdgeOrientation so)
        {
            Adjacent.Add(hex);
            _originalHexagon = hex;
            _originalOrientation = so;
            Position = new EdgePosition(_originalHexagon.Position, _originalOrientation);
            _cache = new EdgeCache(this);
        }

        public Color? Color { get; internal set; }

        public override string ToString()
        {
            return string.Format("Edge [{0}, {1}, {2}]",
                                 _originalHexagon.Position, _originalOrientation, Color);
        }

        internal override void Reset()
        {
            Color = null;
        }

        internal void FindAllAdjacents(Board board)
        {
            // from the original hex
            for (int i = 0; i < BoardConstants.EdgeOrentationCount; ++i)
            {
                var edgeOrientation = (EdgeOrientation) i;
                if (_originalHexagon[edgeOrientation] == this)
                {
                    // add edges
                    Adjacent.Add(
                        (from eo in EdgeStaticInfo.Get(edgeOrientation).AdjacentEdgeOrientations
                         select _originalHexagon[eo])
                        );
                    // add vertices
                    Adjacent.Add(
                        (from vo in EdgeStaticInfo.Get(edgeOrientation).AdjacentVertexOrientations
                         select _originalHexagon[vo])
                        );
                }
            }
            // query 3 other hexes, to add edges
            foreach (EdgePosition edgeOffset in EdgeStaticInfo.Get(_originalOrientation).AdjacentEdgeOffsets)
            {
                Position hexPos = _originalHexagon.Position.Add(edgeOffset.HexPosition.X, edgeOffset.HexPosition.Y);
                Hexagon hex = board[hexPos];
                if (hex != null)
                {
                    Adjacent.Add(hex[edgeOffset.Orientation]);
                }
            }
        }

        internal void Cache()
        {
            _cache.Cache();
        }

        public IEnumerable<Edge> GetAdjacentEdgesSharedWith(Vertex vertex)
        {
            return _cache.GetAdjacentEdgesSharedWith(vertex);
        }

        public bool ConnectsBothEndWithSameColorEdges()
        {
            int count = Adjacent.Vertices.Count(vertex =>
                                                GetAdjacentEdgesSharedWith(vertex).Any(e => e.Color == Color));
            return count == 2;
        }
    }
}