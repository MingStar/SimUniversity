using MingStar.SimUniversity.Contract;

namespace MingStar.SimUniversity.Board.Constructor
{
    internal static class HexagonExtensions
    {
        public static Position GetPositionNextTo(this Hexagon hex, EdgeOrientation so)
        {
            return hex.Position.Add(EdgeStaticInfo.Get(so).HexagonOffset);
        }
    }
}