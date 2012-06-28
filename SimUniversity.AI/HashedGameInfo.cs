namespace MingStar.SimUniversity.AI
{
    public class HashedGameInfo
    {
        public int Depth { get; set; }
        public GameState BestMoves { get; set; }
        public uint AccessCount { get; set; }
    }
}