using System.Collections.Generic;

namespace MingStar.SimUniversity.Contract
{
    public interface IUniversity
    {
        HashSet<IEdge> InternetLinks { get; }
        int NumberOfSuccessfulCompanies { get; }
        DegreeCount ProductionChances { get; }
        bool HasNormalTradingSite { get; }
        HashSet<ISpecialTradingSite> SpecialSites { get; }
        HashSet<IVertex> Campuses { get; }
        HashSet<IVertex> SuperCampuses { get; }
        Color Color { get; }
        int NumberOfFailedCompanies { get; }
        int LengthOfLongestLink { get; }
        DegreeCount Students { get; }
        bool HasStudentsFor(params StudentGroup[] studentGroup);
    }
}