using MingStar.SimUniversity.Contract;

namespace MingStar.SimUniversity.AI.Evaluation
{
    public class ExpansionGameScores : GameScores
    {
        public ExpansionGameScores()
        {
            PlayerScoreBase = 150;
            ProductionBase = 100;
            StudentNumber = 3;
            FutureCampus = 30;
            SpecialSiteMultiplier = 10;
            NormalSite = 200;
            InternetLinkMultiplier = 5;
            HasAllDegrees = 100;
            TakenOtherPlayerCampus = 100;
            LeadMostScore = 200;

            DegreeModifier[DegreeType.Brick] = 1.3;
            DegreeModifier[DegreeType.Wood] = 1.3;
        }
    }
}