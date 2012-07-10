namespace MingStar.SimUniversity.AI.Evaluation
{
    public class ProductionGameScores : GameScores
    {
        public ProductionGameScores()
        {
            ProductionBase = 200;
            StudentNumber = 3;
            FutureCampus = 60;
            SpecialSiteMultiplier = 20;
            NormalSite = 500;
            InternetLinkMultiplier = 10;
            HasAllDegrees = 100;
            TakenOtherPlayerCampus = 100;
            LeadMostScore = 200;
        }
    }
}