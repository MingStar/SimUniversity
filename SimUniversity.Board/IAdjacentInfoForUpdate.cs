using System.Collections.Generic;

namespace MingStar.SimUniversity.Board
{
    internal interface IAdjacentInfoForUpdate
    {
        IEnumerable<Hexagon> Hexagons { get; }
        IEnumerable<Edge> Edges { get; }
        IEnumerable<Vertex> Vertices { get; }
        void Add(Hexagon hex);
        void Add(Vertex vertex);
        void Add(Edge edge);
        void Add(IEnumerable<Edge> edges);
        void Add(IEnumerable<Vertex> vertices);
    }
}