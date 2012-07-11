using System.Collections.ObjectModel;
using MingStar.SimUniversity.Contract;

namespace MingStar.SimUniversity.Game
{
    internal class TurnInfo
    {
        public ReadOnlyCollection<IPlayerMoveForUpdate> AllMoves { get; private set; }
        public int CurrentUnversityLongestLink { get; private set; }
        public ulong Hash { get; private set; }
        public IMostInfo MostFailedStartUps { get; private set; }
        public IMostInfo MostInternetLinks { get; private set; }
        public IPlayerMoveForUpdate Move { get; private set; }

        internal static TurnInfo Create(Game game, IPlayerMoveForUpdate move)
        {
            return new TurnInfo
                       {
                           Move = move,
                           AllMoves = game.GenerateAllMoves(),
                           MostFailedStartUps = game.MostFailedStartUps,
                           MostInternetLinks = game.LongestInternetLink,
                           Hash = game.Hash,
                           CurrentUnversityLongestLink = game.CurrentUniversity.LengthOfLongestLink,
                       };
        }
    }
}