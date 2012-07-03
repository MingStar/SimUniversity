using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace MingStar.SimUniversity.Board
{
    public class AdjacentInfo
    {
        private readonly HashSet<Hexagon> _hexagons = new HashSet<Hexagon>();
        private readonly HashSet<Edge> _edges = new HashSet<Edge>();
        private readonly HashSet<Vertex> _vertices = new HashSet<Vertex>();
        public ReadOnlyCollection<Hexagon> Hexagons { get; private set; }
        public ReadOnlyCollection<Edge> Edges { get; private set; }
        public ReadOnlyCollection<Vertex> Vertices { get; private set; }

        public void Add(Hexagon hex)
        {
            if (hex == null)
            {
                return;
            }
            _hexagons.Add(hex);
            Hexagons = _hexagons.ToList().AsReadOnly();
        }

        public override string ToString()
        {
            return string.Format("Adjacent: [hex: {0} ver: {1} edge: {2}]",
                                 Hexagons.Count, Vertices.Count, Edges.Count);
        }


        public void Add(Edge edge)
        {
            if (edge == null)
            {
                return;
            }
            _edges.Add(edge);
            Edges = _edges.ToList().AsReadOnly();
        }

        public void Add(IEnumerable<Edge> edges)
        {
            _edges.UnionWith(edges);
            Edges = _edges.ToList().AsReadOnly();
        }

        public void Add(Vertex vertex)
        {
            if (vertex == null)
            {
                return;
            }
            _vertices.Add(vertex);
            Vertices = _vertices.ToList().AsReadOnly();
        }

        public void Add(IEnumerable<Vertex> vertices)
        {
            _vertices.UnionWith(vertices);
            Vertices = _vertices.ToList().AsReadOnly();
        }
    }
}