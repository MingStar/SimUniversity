using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MingStar.SimUniversity.Contract;

namespace MingStar.SimUniversity.Board
{
    public interface IHexagonForUpdate
    {
        Vertex this[VertexOrientation vo] { get; set; }
        Edge this[EdgeOrientation eo] { get; set; }
    }
}
