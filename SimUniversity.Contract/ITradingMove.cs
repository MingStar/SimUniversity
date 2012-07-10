namespace MingStar.SimUniversity.Contract
{
    public interface ITradingMove
    {
        DegreeType TradeOut { get; }
        int OutQuantity { get; }
        DegreeType TradeIn { get; }
    }
}