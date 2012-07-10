using MingStar.SimUniversity.Contract;

namespace MingStar.SimUniversity.Game
{
    public interface IGameForUpdate
    {
        void BuildLink(EdgePosition whereAt);
        void BuildCampus(VertexPosition whereAt, CampusType campusType);
        void TradeInStudent(DegreeType tradedIn);
        void TryStartUp(bool isSuccessful);

        void UndoBuildLink(EdgePosition whereAt);
        void UndoBuildCampus(VertexPosition whereAt);
        void UnTradeInStudent(DegreeType tradedIn);
        void UndoTryStartUp(bool isSuccessful);
        void UndoEndTurn(int diceTotal, EnrolmentInfo enrolmentInfo);
    }
}