namespace MingStar.SimUniversity.Contract
{
    public interface IHexagon : IPlace
    {
        Position Position { get; }
        IVertex this[VertexOrientation vo] { get; }
        IEdge this[EdgeOrientation eo] { get; }
        DegreeType Degree { get; }
        int ProductionNumber { get; }
    }
}