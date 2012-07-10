using System.Collections.Generic;
using System.Linq;
using MingStar.SimUniversity.Contract;

namespace MingStar.SimUniversity.Board.Cache
{
    public class EdgeCache
    {
        private readonly IEdge _edge;
        private readonly Dictionary<IVertex, List<IEdge>> _edgesSharingVertex;

        public EdgeCache(IEdge edge)
        {
            _edge = edge;
            _edgesSharingVertex = new Dictionary<IVertex, List<IEdge>>();
        }

        public void Cache()
        {
            foreach (var vertex in _edge.Adjacent.Vertices)
            {
                _edgesSharingVertex[vertex] = new List<IEdge>();
                foreach (var edge in _edge.Adjacent.Edges)
                {
                    if (edge.Adjacent.Vertices.Contains(vertex))
                    {
                        _edgesSharingVertex[vertex].Add(edge);
                    }
                }
            }
        }

        internal IEnumerable<IEdge> GetAdjacentEdgesSharedWith(IVertex vertex)
        {
            return _edgesSharingVertex[vertex];
        }
    }
}