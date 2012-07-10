namespace MingStar.SimUniversity.Contract
{
    public interface ISpecialTradingSite : ITradingSite
    {
        DegreeType TradeOutDegree { get; }
        StudentGroup StudentsNeeded { get; }
    }
}