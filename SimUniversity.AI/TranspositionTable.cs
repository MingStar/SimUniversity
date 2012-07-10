using System.Collections.Generic;

namespace MingStar.SimUniversity.AI
{
    public class TranspositionTable
    {
        private readonly Dictionary<ulong, HashedGameInfo> _hashLookUp =
            new Dictionary<ulong, HashedGameInfo>();

        internal GameState GetBestScoredMoves(ulong hash, int minDepth)
        {
            HashedGameInfo info;
            _hashLookUp.TryGetValue(hash, out info);
            if (info != null && info.Depth >= minDepth)
            {
                ++info.AccessCount;
                return info.BestMoves;
            }
            return null;
        }

        internal void Remember(ulong hash, GameState bestMoves, int depth)
        {
            if (GetBestScoredMoves(hash, depth) == null)
            {
                _hashLookUp[hash] = new HashedGameInfo
                                        {
                                            BestMoves = bestMoves,
                                            Depth = depth
                                        };
            }
        }
    }
}