using System.Collections.Generic;
using MingStar.SimUniversity.Contract;

namespace MingStar.SimUniversity.AI.Player
{
    public class Random : IPlayer
    {
        #region IPlayer Members

        public List<IPlayerMove> MakeMoves(IGame game)
        {
            return new List<IPlayerMove> { game.RandomMove };
        }

        public string Name
        {
            get { return "Random"; }
        }

        #endregion
    }
}