namespace MingStar.SimUniversity.Contract
{
    public interface IBuildLinkMove : IPlayerMove
    {
        EdgePosition WhereAt { get; }
    }
}