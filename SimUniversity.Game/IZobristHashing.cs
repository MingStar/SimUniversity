using MingStar.SimUniversity.Contract;

namespace MingStar.SimUniversity.Game
{
    internal interface IZobristHashing
    {
        ulong Hash { get; set; }
        void HashCurrentUniversity(Color color);
        void HashDegree(Color color, DegreeType degree, int quantity);
        void HashEdge(Color color, EdgePosition ePos);
        void HashMostInfo(IUniversity uni, MostInfoType type);
        void HashStartUp(Color color, bool isSuccessful, int quantity);
        void HashVertex(Color color, VertexPosition vPos, CampusType type);
        ulong NextNewInt64();
    }
}