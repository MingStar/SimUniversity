namespace MingStar.SimUniversity.Contract
{
    public interface IVertex : IPlace
    {
        VertexPosition Position { get; }
        Campus Campus { get; }
        ITradingSite TradingSite { get; }
        bool IsFreeToBuildCampus();
    }
}