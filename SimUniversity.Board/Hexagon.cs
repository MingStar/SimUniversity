using MingStar.SimUniversity.Contract;

namespace MingStar.SimUniversity.Board
{
    public class Hexagon : Place, IHexagon, IHexagonForUpdate
    {
        #region Public Properties

        public Position Position { get; private set; }
        public int ProductionNumber { get; private set; }
        public DegreeType Degree { get; private set; }

        #endregion

        #region Private Fields - for faster look up than adjacent info

        private readonly Edge[] _edges = new Edge[6]; // 6 edges
        private readonly Vertex[] _vertices = new Vertex[6]; // 6 vertices

        #endregion

        public Hexagon(int id, DegreeType degree, Position position)
        {
            ProductionNumber = id;
            Degree = degree;
            Position = position;
        }

        #region IHexagon Members

        IVertex IHexagon.this[VertexOrientation vo]
        {
            get { return _vertices[(int) vo]; }
        }

        IEdge IHexagon.this[EdgeOrientation eo]
        {
            get { return _edges[(int) eo]; }
        }

        #endregion

        #region IHexagonForUpdate Members

        public Vertex this[VertexOrientation vo]
        {
            get { return _vertices[(int) vo]; }
            set { _vertices[(int) vo] = value; }
        }

        public Edge this[EdgeOrientation eo]
        {
            get { return _edges[(int) eo]; }
            set { _edges[(int) eo] = value; }
        }

        #endregion

        public override string ToString()
        {
            return string.Format("Hexagon [{0}, {1}, {2}]", ProductionNumber, Degree, Position);
        }

        public override void Reset()
        {
            //do nothing
        }
    }
}