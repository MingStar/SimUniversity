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
        GamePhase CurrentPhase { get; }

        bool HasWinner();
        IEnumerable<IPlayerMove> GenerateAllMoves();
        int GetScore(IUniversity uni);
        int GetVertexProductionChance(IVertex vertex);
    }
}