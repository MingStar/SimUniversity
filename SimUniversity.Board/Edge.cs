using System.Collections.Generic;
using System.Linq;
using MingStar.SimUniversity.Board.Cache;
using MingStar.SimUniversity.Contract;

namespace MingStar.SimUniversity.Board
{
    public class Edge : Place
    {
        public readonly EdgePosition Position;

        private readonly Hexagon _originalHexagon;
        private readonly EdgeOrientation _originalOrientation;
        private readonly EdgeCache _cache;

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
                var thisOrientation = (EdgeOrientation)i;
                if (_originalHexagon[thisOrientation] == this)
                {
                    // add edges
                    this.Adjacent.Add(
                        (from eo in EdgeStaticInfo.Get(thisOrientation).AdjacentEdgeOrientations
                         select _originalHexagon[eo])
                        );
                    // add vertices
                    this.Adjacent.Add(
                        (from vo in EdgeStaticInfo.Get(thisOrientation).AdjacentVertexOrientations
                         select _originalHexagon[vo])
                        );
                }
            }
            // 3 other hex, to add edges
            foreach (EdgePosition edgeOffset in EdgeStaticInfo.Get(_originalOrientation).AdjacentEdgeOffsets)
            {
                var pos = _originalHexagon.Position.Add(edgeOffset.HexPosition.X, edgeOffset.HexPosition.Y);
                var hex = board[pos];
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
            var count = Adjacent.Vertices.Count(vertex => 
                GetAdjacentEdgesSharedWith(vertex).Any(e => e.Color == Color));
            return count == 2;
        }
    }
}