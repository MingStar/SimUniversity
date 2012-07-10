namespace MingStar.SimUniversity.Contract
{
    public interface IBuildCampusMove
    {
        VertexPosition WhereAt { get; }
        CampusType CampusType { get; }
    }
}