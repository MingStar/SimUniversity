using System.Collections.ObjectModel;
using MingStar.SimUniversity.Contract;

namespace MingStar.SimUniversity.Game
{
    public class TurnInfo
    {
        public ReadOnlyCollection<IPlayerMove> AllMoves;
        public int CurrentUnversityLongestLink;
        public ulong Hash;
        public MostInfo MostFailedStartUps;
        public MostInfo MostInternetLinks;
        public IPlayerMove Move;

        internal static TurnInfo Create(Game game, IPlayerMove move)
        {
            return new TurnInfo
                       {
                           Move = move,
                           AllMoves = game.AllMoves,
                           MostFailedStartUps = game.MostFailedStartUps,
                           MostInternetLinks = game.LongestInternetLink,
                           Hash = game.Hash,
                           CurrentUnversityLongestLink = game.CurrentUniversity.LengthOfLongestLink,
                       };
        }
    }
}