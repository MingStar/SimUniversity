using MingStar.SimUniversity.Contract;

namespace MingStar.SimUniversity.Board
{
    public class Vertex : Place, IVertex, IVertextForUpdate
    {
        #region Public Read-Only Properties

        public int NumberOfNeighbourCampuses { get; private set; }
        public VertexPosition Position { get; private set; }
        public Campus Campus { get; private set; }
        public ITradingSite TradingSite { get; private set; }

        #endregion

        #region Private Fields

        private readonly Hexagon _originalHexagon;
        private readonly VertexOrientation _originalOrientation;

        #endregion

        #region Constructor

        public Vertex(Hexagon hex, VertexOrientation vo)
        {
            AdjacentForUpdate.Add(hex);
            _originalHexagon = hex;
            _originalOrientation = vo;
            Position = new VertexPosition(hex.Position, vo);
            Reset();
        }

        #endregion

        #region Public Override Methods

        public override string ToString()
        {
            return string.Format("Vertex [{0}, {1}, {2}, {3}]",
                                 _originalHexagon.Position, _originalOrientation, TradingSite, Campus);
        }

        #endregion

        #region IVertex Members

        public bool IsFreeToBuildCampus()
        {
            return Campus == null && NumberOfNeighbourCampuses == 0;
        }

        #endregion

        #region IVertextForUpdate Members

        public override void Reset()
        {
            Campus = null;
            NumberOfNeighbourCampuses = 0;
        }

        public void BuildCampus(CampusType type, Color color)
        {
            Campus = new Campus(type, color);
            if (type == CampusType.Traditional)
            {
                foreach (Vertex adj in Adjacent.Vertices)
                {
                    adj.NumberOfNeighbourCampuses += 1;
                }
            }
        }

        public void MakeSpecialSite(DegreeType degree)
        {
            TradingSite = new SpecialTradingSite(degree);
        }

        public void MakeMultiSite()
        {
            TradingSite = SimUniversity.Board.TradingSite.Instance;
        }

        public void DowngradeCampus()
        {
            if (Campus == null)
            {
                return;
            }
            if (Campus.Type == CampusType.Super)
            {
                Campus = new Campus(CampusType.Traditional, Campus.Color);
            }
            else // == CampusType.Traditional)
            {
                Campus = null;
                foreach (Vertex adj in Adjacent.Vertices)
                {
                    adj.NumberOfNeighbourCampuses -= 1;
                }
            }
        }

        #endregion
    }
}