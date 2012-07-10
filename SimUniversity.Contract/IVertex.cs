namespace MingStar.SimUniversity.Contract
{
    public interface IVertex : IPlace
    {
        VertexPosition Position { get; }
        Campus Campus { get; }
        ITradingSite TradingSite { get; }
        bool IsFreeToBuildCampus();

        #region Commands
        void MakeMultiSite();
        void MakeSpecialSite(DegreeType degree);
        void DowngradeCampus();
        void BuildCampus(CampusType type, Color color);
        void Reset();
        #endregion
    }
}