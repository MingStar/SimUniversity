using System.Collections.Generic;

namespace MingStar.SimUniversity.Contract
{
    public interface IEdge : IPlace
    {
        Color? Color { get; set; }
        void Reset();
        bool ConnectsBothEndWithSameColorEdges();
        IEnumerable<IEdge> GetAdjacentEdgesSharedWith(IVertex useVertex);

        EdgePosition Position { get; }

        void FindAllAdjacents(IBoard Board);
        void Cache();
    }
}