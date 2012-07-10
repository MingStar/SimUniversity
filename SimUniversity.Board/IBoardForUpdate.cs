using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MingStar.SimUniversity.Contract;

namespace MingStar.SimUniversity.Board
{
    public interface IBoardForUpdate
    {
        Hexagon this[Position pos] { get; }
    }
}
