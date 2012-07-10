using System.Collections.Generic;

namespace MingStar.SimUniversity.Contract
{
    public interface IEdge : IPlace
    {
        EdgePosition Position { get; }
        Color? Color { get; }
        bool ConnectsBothEndWithSameColorEdges();
        IEnumerable<IEdge> GetAdjacentEdgesSharedWith(IVertex vertex);
    }
}