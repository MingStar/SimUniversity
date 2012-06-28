using MingStar.SimUniversity.Contract;

namespace MingStar.SimUniversity.Board
{
    public class Vertex : Place
    {
        public static int TotalCount { get; private set; }

        #region Public Read-Only Properties

        public VertexPosition Position { get; private set; }
        public Campus Campus { get; private set; }
        public TradingSite TradingSite { get; private set; }
        public int NumberOfNeighbourCampuses { get; private set; }

        #endregion

        #region Private Fields

        private readonly Hexagon m_originalHexagon;
        private readonly VertexOrientation m_originalOrientation;

        #endregion

        #region Constructor

        public Vertex(Hexagon hex, VertexOrientation vo)
        {
            Adjacent.Add(hex);
            m_originalHexagon = hex;
            m_originalOrientation = vo;
            Position = new VertexPosition(hex.Position, vo);
            Reset();
            ++TotalCount;
        }

        #endregion

        #region Public Override Methods

        public override string ToString()
        {
            return string.Format("Vertex [{0}, {1}, {2}, {3}]",
                                 m_originalHexagon.Position, m_originalOrientation, TradingSite, Campus);
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

        internal void FindAdjacents()
        {
            foreach (Hexagon hex in Adjacent.Hexagons)
            {
                hex.FindAdjacentsFor(this);
            }
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
            TradingSite = TradingSite.Instance;
        }

        internal void DowngradeCampus()
        {
            if (Campus != null)
            {
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
}