using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MingStar.SimUniversity.Contract;

namespace MingStar.SimUniversity.Board
{
    internal interface IAdjacentInfoForUpdate
    {
        void Add(IHexagon hex);
        void Add(IVertex vertex);
        void Add(IEdge edge);
        void Add(IEnumerable<IEdge> edges);
        void Add(IEnumerable<IVertex> vertices);
    }
}
