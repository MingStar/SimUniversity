using MingStar.SimUniversity.Contract;

namespace MingStar.SimUniversity.Board
{
    public interface IBoardForUpdate
    {
        Hexagon this[Position pos] { get; }
    }
}