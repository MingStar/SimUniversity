using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using MingStar.SimUniversity.Contract;

namespace MingStar.SimUniversity.Board
{
    public class AdjacentInfo : IAdjacentInfo, IAdjacentInfoForUpdate
    {
        private readonly HashSet<IEdge> _edges = new HashSet<IEdge>();
        private readonly HashSet<IHexagon> _hexagons = new HashSet<IHexagon>();
        private readonly HashSet<IVertex> _vertices = new HashSet<IVertex>();

        public IEnumerable<IHexagon> Hexagons { get { return _hexagons; } }
        public IEnumerable<IEdge> Edges { get { return _edges; } }
        public IEnumerable<IVertex> Vertices { get { return _vertices; } }

        public void Add(IHexagon hex)
        {
            if (hex == null)
            {
                return;
            }
            _hexagons.Add(hex);
        }

        public override string ToString()
        {
            return string.Format("Adjacent: [hex: {0}, ver: {1}, edge: {2}]",
                                 _hexagons.Count, _vertices.Count, _edges.Count);
        }


        public void Add(IEdge edge)
        {
            if (edge == null)
            {
                return;
            }
            _edges.Add(edge);
        }

        public void Add(IEnumerable<IEdge> edges)
        {
            _edges.UnionWith(edges);
        }

        public void Add(IVertex vertex)
        {
            if (vertex == null)
            {
                return;
            }
            _vertices.Add(vertex);
        }

        public void Add(IEnumerable<IVertex> vertices)
        {
            _vertices.UnionWith(vertices);
        }
    }
}