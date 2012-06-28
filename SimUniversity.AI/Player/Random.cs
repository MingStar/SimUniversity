using System.Collections.Generic;
using MingStar.SimUniversity.Contract;
using MingStar.SimUniversity.Game.Move;

namespace MingStar.SimUniversity.AI.Player
{
    public class Random : IPlayer
    {
        #region IPlayer Members

        public List<IPlayerMove> MakeMoves(IGame game)
        {
            return new List<IPlayerMove> {new RandomMove()};
        }

        public string Name
        {
            get { return "Random"; }
        }

        #endregion
    }
}