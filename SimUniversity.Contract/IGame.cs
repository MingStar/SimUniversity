using System.Collections.ObjectModel;

namespace MingStar.SimUniversity.Contract
{
    public interface IGame : IGameControllable
    {
        Color CurrentUniversityColor { get; }
        bool HasHumanPlayer { get; }
        void BuildLink(EdgePosition whereAt);
        void BuildCampus(VertexPosition whereAt, CampusType campusType);
        void TradeInStudent(DegreeType tradedIn);
        void TryStartUp(bool isSuccessful);

        bool IsLegalToBuildLink(EdgePosition pos);
        bool IsLegalToBuildCampus(VertexPosition whereAt, CampusType type);

        ReadOnlyCollection<IPlayerMove> GenerateAllMoves();

        #region Undo Moves

        void UndoMove();
        void UndoBuildLink(EdgePosition whereAt);
        void UndoBuildCampus(VertexPosition whereAt);
        void UnTradeInStudent(DegreeType tradedIn);
        void UndoTryStartUp(bool isSuccessful);
        void UndoEndTurn(int diceTotal, EnrolmentInfo enrolmentInfo);

        #endregion
    }
}