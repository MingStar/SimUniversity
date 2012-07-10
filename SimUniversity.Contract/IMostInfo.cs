namespace MingStar.SimUniversity.Contract
{
    public interface IMostInfo
    {
        int Threshold { get; }
        IUniversity University { get; }
        int Number { get; }
        IMostInfo GetMore(IUniversity uni, int number);
    }
}