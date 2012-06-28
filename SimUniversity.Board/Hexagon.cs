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

        private readonly Edge[] _edges = new Edge[6]; // 6 edges
        private readonly Hexagon[] _hexagons = new Hexagon[6]; // 1 to 6 hexagons
        private readonly Vertex[] _vertices = new Vertex[6]; // 6 vertices

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

        #endregion

        #region Board Construction Related

        internal Position GetPositionNextTo(EdgeOrientation so)
        {
            return Position.Add(EdgeStaticInfo.Get(so).HexagonOffset);
        }

        internal void PlaceEnd(Board board)
        {
            // set adjacent hexagons
            for (int i = 0; i < Constant.EdgeOrentationCount; ++i)
            {
                if (_hexagons[i] != null) 
                    continue;
                var eo = (EdgeOrientation) i;
                var hex = board[GetPositionNextTo(eo)];
                if (hex != null)
                {
                    AtSideJoin(eo, hex);
                }
            }
            // create vertices
            for (int i = 0; i < Constant.VertexOrentationCount; ++i)
            {
                if (_vertices[i] == null)
                {
                    var vertex = UseOtherOrCreateVertex(board, (VertexOrientation) i);
                    _vertices[i] = vertex;
                    Adjacent.Add(vertex);
                    vertex.Adjacent.Add(this);
                }
            }
            // create edges 
            for (int i = 0; i < Constant.EdgeOrentationCount; ++i)
            {
                if (_edges[i] != null) 
                    continue;
                Edge edge = UseOtherOrCreateEdge(board, (EdgeOrientation) i);
                _edges[i] = edge;
                Adjacent.Add(edge);
                edge.Adjacent.Add(this);
            }
        }

        private void AtSideJoin(EdgeOrientation eo, Hexagon hex)
        {
            AddAdjacent(hex, eo);
            hex.AddAdjacent(this, EdgeStaticInfo.Get(eo).OppositeEdge);
        }

        private void AddAdjacent(Hexagon hex, EdgeOrientation eo)
        {
            _hexagons[(int) eo] = hex;
            Adjacent.Add(hex);
        }

        private Vertex UseOtherOrCreateVertex(Board board, VertexOrientation vo)
        {
            // can be 2 adjacent hexagons
            foreach (var pos in VertexStaticInfo.Get(vo).RelativePositions)
            {
                var hex = board[Position.Add(pos.Offset)];
                if (hex == null) 
                    continue;
                var vertex = hex[pos.Orientation];
                if (vertex != null)
                {
                    return vertex;
                }
            }
            return new Vertex(this, vo);
        }

        private Edge UseOtherOrCreateEdge(Board board, EdgeOrientation eo)
        {
            // only one adjacent hexagon
            Hexagon hex = board[GetPositionNextTo(eo)];
            if (hex != null)
            {
                Edge edge = hex[EdgeStaticInfo.Get(eo).OppositeEdge];
                if (edge != null)
                {
                    return edge;
                }
            }
            return new Edge(this, eo);
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