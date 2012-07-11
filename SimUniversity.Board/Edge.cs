using System.Collections.Generic;
using System.Linq;
using MingStar.SimUniversity.Board.Cache;
using MingStar.SimUniversity.Board.Positioning;
using MingStar.SimUniversity.Contract;

namespace MingStar.SimUniversity.Board
{
    public class Edge : Place, IEdge
    {
        private readonly EdgeCache _cache;
        private readonly Hexagon _originalHexagon;
        private readonly EdgeOrientation _originalOrientation;

        public Edge(Hexagon hex, EdgeOrientation so)
        {
            AdjacentForUpdate.Add(hex);
            _originalHexagon = hex;
            _originalOrientation = so;
            Position = new EdgePosition(_originalHexagon.Position, _originalOrientation);
            _cache = new EdgeCache(this);
        }

        #region IEdge Members

        public EdgePosition Position { get; private set; }

        public Color? Color { get; private set; }

        public IEnumerable<IEdge> GetAdjacentEdgesSharedWith(IVertex vertex)
        {
            return _cache.GetAdjacentEdgesSharedWith(vertex);
        }

        public bool ConnectsBothEndWithSameColorEdges()
        {
            int count = Adjacent.Vertices.Count(vertex =>
                                                GetAdjacentEdgesSharedWith(vertex).Any(e => e.Color == Color));
            return count == 2;
        }

        #endregion

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
                    AdjacentForUpdate.Add(
                        (from eo in EdgeStaticInfo.Get(edgeOrientation).AdjacentEdgeOrientations
                         select _originalHexagon[eo])
                        );
                    // add vertices
                    AdjacentForUpdate.Add(
                        (from vo in EdgeStaticInfo.Get(edgeOrientation).AdjacentVertexOrientations
                         select _originalHexagon[vo])
                        );
                }
            }
            // query 3 other hexes, to add edges
            foreach (var edgeOffset in EdgeStaticInfo.Get(_originalOrientation).AdjacentEdgeOffsets)
            {
                var hexPos = _originalHexagon.Position.Add(edgeOffset.HexPosition.X, edgeOffset.HexPosition.Y);
                var hex = board[hexPos];
                if (hex != null)
                {
                    AdjacentForUpdate.Add(hex[edgeOffset.Orientation]);
                }
            }
        }

        internal void Cache()
        {
            _cache.Cache();
        }

        internal void SetColor(Color color)
        {
            Color = color;
        }

        public override string ToString()
        {
            return string.Format("Edge [{0}, {1}, {2}]",
                                 _originalHexagon.Position, _originalOrientation, Color);
        }
    }
}