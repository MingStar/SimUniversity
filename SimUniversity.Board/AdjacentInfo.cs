using System.Collections.Generic;
using MingStar.SimUniversity.Contract;

namespace MingStar.SimUniversity.Board
{
    public class AdjacentInfo : IAdjacentInfo
    {
        private readonly HashSet<Edge> _edges = new HashSet<Edge>();
        private readonly HashSet<Hexagon> _hexagons = new HashSet<Hexagon>();
        private readonly HashSet<Vertex> _vertices = new HashSet<Vertex>();

        #region IAdjacentInfo Members

        IEnumerable<IHexagon> IAdjacentInfo.Hexagons
        {
            get { return _hexagons; }
        }

        IEnumerable<IEdge> IAdjacentInfo.Edges
        {
            get { return _edges; }
        }

        IEnumerable<IVertex> IAdjacentInfo.Vertices
        {
            get { return _vertices; }
        }
        #endregion

        internal IEnumerable<Hexagon> Hexagons
        {
            get { return _hexagons; }
        }

        internal IEnumerable<Edge> Edges
        {
            get { return _edges; }
        }

        internal IEnumerable<Vertex> Vertices
        {
            get { return _vertices; }
        }

        internal void Add(Hexagon hex)
        {
            if (hex == null)
            {
                return;
            }
            _hexagons.Add(hex);
        }

        internal void Add(Edge edge)
        {
            if (edge == null)
            {
                return;
            }
            _edges.Add(edge);
        }

        internal void Add(IEnumerable<Edge> edges)
        {
            _edges.UnionWith(edges);
        }

        internal void Add(Vertex vertex)
        {
            if (vertex == null)
            {
                return;
            }
            _vertices.Add(vertex);
        }

        internal void Add(IEnumerable<Vertex> vertices)
        {
            _vertices.UnionWith(vertices);
        }

        public override string ToString()
        {
            return string.Format("Adjacent: [hex: {0}, ver: {1}, edge: {2}]",
                                 _hexagons.Count, _vertices.Count, _edges.Count);
        }
    }
}