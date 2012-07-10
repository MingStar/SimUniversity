using MingStar.SimUniversity.Contract;

namespace MingStar.SimUniversity.Board
{
    public interface IVertextForUpdate
    {
        void MakeMultiSite();
        void MakeSpecialSite(DegreeType degree);
        void DowngradeCampus();
        void BuildCampus(CampusType type, Color color);
        void Reset();         
    }
}