using System.Linq;
using MingStar.SimUniversity.Contract;

namespace MingStar.SimUniversity.Board
{
    public class Hexagon : Place
    {
        #region Public ReadOnly Properties

        public Position Position { get; private set; }
        public int ProductionNumber { get; private set; }
        public DegreeType Degree { get; private set; }

        #endregion

        #region Private Fields

        internal readonly Edge[] Edges = new Edge[6]; // 6 edges
        internal readonly Hexagon[] Hexagons = new Hexagon[6]; // 1 to 6 hexagons
        internal readonly Vertex[] Vertices = new Vertex[6]; // 6 vertices

        #endregion

        #region Construtor

        public Hexagon(int id, DegreeType degree, Position position)
        {
            ProductionNumber = id;
            Degree = degree;
            Position = position;
        }

        #endregion

        #region Public Override Methods

        public override string ToString()
        {
            return string.Format("Hexagon [{0}, {1}, {2}]", ProductionNumber, Degree, Position);
        }

        internal override void Reset()
        {
            //do nothing
        }

        #endregion

        #region Public Method

        public Vertex this[VertexOrientation vo]
        {
            get { return Vertices[(int) vo]; }
        }

        public Edge this[EdgeOrientation so]
        {
            get { return Edges[(int) so]; }
        }

        internal Position GetPositionNextTo(EdgeOrientation so)
        {
            return this.Position.Add(EdgeStaticInfo.Get(so).HexagonOffset);
        }

        internal void AddAdjacent(Hexagon hex, EdgeOrientation eo)
        {
            Hexagons[(int)eo] = hex;
            Adjacent.Add(hex);
        }
        #endregion
    }
}