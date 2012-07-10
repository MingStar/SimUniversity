using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MingStar.SimUniversity.Contract;

namespace MingStar.SimUniversity.Board
{
    internal interface IAdjacentInfoForUpdate
    {
        void Add(Hexagon hex);
        void Add(Vertex vertex);
        void Add(Edge edge);
        void Add(IEnumerable<Edge> edges);
        void Add(IEnumerable<Vertex> vertices);

        IEnumerable<Hexagon> Hexagons { get; }
        IEnumerable<Edge> Edges { get; }
        IEnumerable<Vertex> Vertices { get; }
    }
}
