using MingStar.SimUniversity.Contract;

namespace MingStar.SimUniversity.Board
{
    public class Vertex : Place
    {
        #region Public Read-Only Properties

        public VertexPosition Position { get; private set; }
        public Campus Campus { get; private set; }
        public ITradingSite TradingSite { get; private set; }
        public int NumberOfNeighbourCampuses { get; private set; }

        #endregion

        #region Private Fields

        private readonly Hexagon _originalHexagon;
        private readonly VertexOrientation _originalOrientation;

        #endregion

        #region Constructor

        public Vertex(Hexagon hex, VertexOrientation vo)
        {
            Adjacent.Add(hex);
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

        internal override void Reset()
        {
            Campus = null;
            NumberOfNeighbourCampuses = 0;
        }

        public bool IsFreeToBuildCampus()
        {
            return Campus == null && NumberOfNeighbourCampuses == 0;
        }

        internal void BuildCampus(CampusType type, Color color)
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

        internal void MakeSpecialSite(DegreeType degree)
        {
            TradingSite = new SpecialTradingSite(degree);
        }

        internal void MakeMultiSite()
        {
            TradingSite = SimUniversity.Board.TradingSite.Instance;
        }

        internal void DowngradeCampus()
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
    }
}