using MingStar.SimUniversity.Contract;

namespace MingStar.SimUniversity.Board
{
    /// <summary>
    /// base class for hexagons, edges and vertices
    /// </summary>
    public abstract class Place : IPlace
    {
        private readonly AdjacentInfo _adjacentInfo;

        protected Place()
        {
            _adjacentInfo = new AdjacentInfo();
        }

        internal IAdjacentInfoForUpdate AdjacentForUpdate
        {
            get { return _adjacentInfo; }
        }

        #region IPlace Members

        public IAdjacentInfo Adjacent
        {
            get { return _adjacentInfo; }
        }

        #endregion

        public abstract void Reset();
    }
}