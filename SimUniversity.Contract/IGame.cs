using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MingStar.SimUniversity.Contract
{
    public interface IGame : IGameControllable
    {
        Color CurrentUniversityColor { get; }
        int CurrentUniversityIndex { get; }
        int NumberOfUniversities { get; }
        IUniversity CurrentIUniversity { get; }
        IEnumerable<IUniversity> Universities { get; }
        IMostInfo MostFailedStartUps { get; }
        IMostInfo LongestInternetLink { get; }
        double ProbabilityWithNoCut { get; }
        IBoard IBoard { get; }
        ulong Hash { get; }
        IGameRules Rules { get; }
        int CurrentTurn { get; }
        Dictionary<DegreeType, double> Scarcity { get; }
        GamePhase CurrentPhase { get; }
        IPlayerMove RandomMove { get; }

        bool HasWinner();
        IUniversity GetUniversityByIndex(int index);
        IEnumerable<IPlayerMove> GenerateAllMoves();
        int GetScore(IUniversity uni);
        int GetVertexProductionChance(IVertex vertex);
    }
}