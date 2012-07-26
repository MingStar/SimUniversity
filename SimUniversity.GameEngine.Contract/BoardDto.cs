using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimUniversity.GameEngine.Contract
{
    public class BoardDto
    {
        public IEnumerable<HexagonDto> Hexagons { get; set; }
    }
}
