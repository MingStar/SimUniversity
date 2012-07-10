using MingStar.SimUniversity.Contract;

namespace MingStar.SimUniversity.Game
{
    internal class NullHashing : IZobristHashing
    {
        #region IZobristHashing Members

        public ulong Hash
        {
            get { return ulong.MinValue; }
            set { }
        }

        public void HashCurrentUniversity(Color color)
        {
        }

        public void HashDegree(Color color, DegreeType degree, int quantity)
        {
        }

        public void HashEdge(Color color, EdgePosition ePos)
        {
        }

        public void HashMostInfo(IUniversity uni, MostInfoType type)
        {
        }

        public void HashStartUp(Color color, bool isSuccessful, int quantity)
        {
        }

        public void HashVertex(Color color, VertexPosition vPos, CampusType type)
        {
        }

        public ulong NextNewInt64()
        {
            return ulong.MinValue;
        }

        #endregion
    }
}