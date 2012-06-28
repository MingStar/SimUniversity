using MingStar.SimUniversity.Contract;

namespace MingStar.SimUniversity.Game.Rules
{
    public class HigherScoreGameRules : IGameRules
    {
        #region IGameRules Members

        public int WinningScore
        {
            get { return 13; }
        }

        #endregion
    }
}