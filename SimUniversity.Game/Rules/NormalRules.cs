using MingStar.SimUniversity.Contract;

namespace MingStar.SimUniversity.Game.Rules
{
    public class NormalRules : IGameRules
    {
        #region IGameRules Members

        public int WinningScore
        {
            get { return 10; }
        }

        #endregion
    }
}