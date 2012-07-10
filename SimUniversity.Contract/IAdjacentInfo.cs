using System.Collections.Generic;

namespace MingStar.SimUniversity.Contract
{
    public interface IAdjacentInfo
    {
        IEnumerable<IHexagon> Hexagons { get; }
        IEnumerable<IEdge> Edges { get; }
        IEnumerable<IVertex> Vertices { get; }
    }
}