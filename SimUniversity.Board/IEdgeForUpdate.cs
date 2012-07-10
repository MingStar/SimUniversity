using MingStar.SimUniversity.Contract;

namespace MingStar.SimUniversity.Board
{
    internal interface IEdgeForUpdate
    {
        void SetColor(Color color);
        void Reset();
        void FindAllAdjacents(Board Board);
        void Cache();
    }
}