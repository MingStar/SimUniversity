using System.Linq;
using MingStar.SimUniversity.Contract;

namespace MingStar.SimUniversity.Board
{
    public class Hexagon : Place
    {
        #region Public ReadOnly Properties

        public Position Position { get; private set; }
        public int ID { get; private set; }
        public DegreeType Degree { get; private set; }

        #endregion

        #region Private Fields

        internal readonly Edge[] _edges = new Edge[6]; // 6 edges
        internal readonly Hexagon[] _hexagons = new Hexagon[6]; // 1 to 6 hexagons
        internal readonly Vertex[] _vertices = new Vertex[6]; // 6 vertices

        #endregion

        #region Construtor

        public Hexagon(int id, DegreeType degree, Position position)
        {
            ID = id;
            Degree = degree;
            Position = position;
        }

        #endregion

        #region Public Override Methods

        public override string ToString()
        {
            return string.Format("Hexagon [{0}, {1}, {2}]", ID, Degree, Position);
        }

        internal override void Reset()
        {
            //do nothing
        }

        #endregion

        #region Public Method

        public Vertex this[VertexOrientation vo]
        {
            get { return _vertices[(int) vo]; }
        }

        public Edge this[EdgeOrientation so]
        {
            get { return _edges[(int) so]; }
        }

        internal Position GetPositionNextTo(EdgeOrientation so)
        {
            return this.Position.Add(EdgeStaticInfo.Get(so).HexagonOffset);
        }

        internal void AddAdjacent(Hexagon hex, EdgeOrientation eo)
        {
            _hexagons[(int)eo] = hex;
            Adjacent.Add(hex);
        }
        #endregion

        internal void FindAdjacentsFor(Edge edge)
        {
            for (int i = 0; i < _edges.Length; ++i)
            {
                if (_edges[i] == edge)
                {
                    var thisOrientation = (EdgeOrientation) i;
                    edge.Adjacent.Add(
                        (from eo in EdgeStaticInfo.Get(thisOrientation).AdjacentEdgeOrientations
                         select this[eo])
                        );
                    edge.Adjacent.Add(
                        (from vo in EdgeStaticInfo.Get(thisOrientation).AdjacentVertexOrientations
                         select this[vo])
                        );
                }
            }
        }

        internal void FindAdjacentsFor(Vertex vertex)
        {
            for (int i = 0; i < _vertices.Length; ++i)
            {
                if (_vertices[i] == vertex)
                {
                    VertexStaticInfo staticInfo = VertexStaticInfo.Get((VertexOrientation) i);
                    vertex.Adjacent.Add(
                        (from adjEO in staticInfo.AdjacentEdgeOrientations
                         select this[adjEO])
                        );
                    vertex.Adjacent.Add(
                        (from adjVO in staticInfo.AdjacentVertexOrientations
                         select this[adjVO])
                        );
                }
            }
        }
    }
}