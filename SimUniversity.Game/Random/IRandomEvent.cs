namespace MingStar.SimUniversity.Game.Random
{
    public interface IRandomEvent
    {
        int GetNextDiceTotal();
        bool IsNextStartUpSuccessful();
    }
}