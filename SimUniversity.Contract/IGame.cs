using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MingStar.SimUniversity.Contract
{
    public interface IGame : IGameControllable
    {
        Color CurrentUniversityColor { get; }
        bool HasHumanPlayer { get; }
        int CurrentUniversityIndex { get; }
        int NumberOfUniversities { get; }
        IUniversity CurrentIUniversity { get; }
        ReadOnlyCollection<IUniversity> Universities { get; }
        IMostInfo MostFailedStartUps { get; }
        IMostInfo LongestInternetLink { get; }
        double ProbabilityWithNoCut { get; }
        IBoard IBoard { get; }
        ulong Hash { get; }
        IGameRules Rules { get; }
        int CurrentTurn { get; }
        Dictionary<DegreeType, double> Scarcity { get; }

        bool IsLegalToBuildLink(EdgePosition pos);
        bool IsLegalToBuildCampus(VertexPosition whereAt, CampusType type);
        ReadOnlyCollection<IPlayerMove> GenerateAllMoves();
        int GetScore(IUniversity uni);
        int GetVertexProductionChance(IVertex vertex);

        #region Commands
        void BuildLink(EdgePosition whereAt);
        void BuildCampus(VertexPosition whereAt, CampusType campusType);
        void TradeInStudent(DegreeType tradedIn);
        void TryStartUp(bool isSuccessful);

        #region Undo Moves
        void UndoMove();
        void UndoBuildLink(EdgePosition whereAt);
        void UndoBuildCampus(VertexPosition whereAt);
        void UnTradeInStudent(DegreeType tradedIn);
        void UndoTryStartUp(bool isSuccessful);
        void UndoEndTurn(int diceTotal, EnrolmentInfo enrolmentInfo);
        #endregion

        #endregion
    }
}