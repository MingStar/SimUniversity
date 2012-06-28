using System.Collections.Generic;
using System.Linq;
using MingStar.SimUniversity.Contract;

namespace MingStar.SimUniversity.Board
{
    public class Edge : Place
    {
        public readonly EdgePosition Position;

        private readonly Dictionary<Vertex, List<Edge>> m_edgesSharingVertex =
            new Dictionary<Vertex, List<Edge>>();

        private readonly Hexagon m_originalHexagon;
        private readonly EdgeOrientation m_originalOrientation;


        public Edge(Hexagon hex, EdgeOrientation so)
        {
            Adjacent.Add(hex);
            m_originalHexagon = hex;
            m_originalOrientation = so;
            Position = new EdgePosition(m_originalHexagon.Position, m_originalOrientation);
            ++TotalCount;
        }

        public static int TotalCount { get; private set; }
        public Color? Color { get; internal set; }

        public override string ToString()
        {
            return string.Format("Edge [{0}, {1}, {2}]",
                                 m_originalHexagon.Position, m_originalOrientation, Color);
        }

        internal override void Reset()
        {
            Color = null;
        }

        internal void FindAllAdjacents(Board board)
        {
            // the original hex
            m_originalHexagon.FindAdjacentsFor(this);
            // 3 other hex, to add edges
            foreach (EdgePosition edgeOffset in EdgeStaticInfo.Get(m_originalOrientation).AdjacentEdgeOffsets)
            {
                var pos = m_originalHexagon.Position.Add(edgeOffset.HexPosition.X, edgeOffset.HexPosition.Y);
                var hex = board[pos];
                if (hex != null)
                {
                    Adjacent.Add(hex[edgeOffset.Orientation]);
                }
            }
        }

        internal void FindAdjacentSharedEdges()
        {
            foreach (var vertex in Adjacent.Vertices)
            {
                m_edgesSharingVertex[vertex] = new List<Edge>();
                foreach (var edge in Adjacent.Edges)
                {
                    if (edge.Adjacent.Vertices.Contains(vertex))
                    {
                        m_edgesSharingVertex[vertex].Add(edge);
                    }
                }
            }
        }

        public List<Edge> GetAdjacentEdgesSharedWith(Vertex vertex)
        {
            return m_edgesSharingVertex[vertex];
        }


        public bool ConnectsEdgesWithSameColor()
        {
            var count = Adjacent.Vertices.Count(vertex => 
                GetAdjacentEdgesSharedWith(vertex).Any(e => e.Color == Color));
            return count == 2;
        }
    }
}