namespace MingStar.SimUniversity.Game.Random
{
    public class RandomEvent : IRandomEvent
    {
        public int GetNextDiceTotal()
        {
            return RandomGenerator.Next(6) + RandomGenerator.Next(6) + 2;
        }

        public bool IsNextStartUpSuccessful()
        {
            return RandomGenerator.Next(5) == 0;
        }
    }
}
