using System.Collections.Generic;

namespace MingStar.SimUniversity.Contract
{
    public interface IBoard
    {
        IEdge this[EdgePosition pos] { get; }
        IVertex this[VertexPosition pos] { get; }
        IHexagon this[Position pos] { get; }
        IEnumerable<IVertex> GetVertices();
    }
}