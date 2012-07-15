namespace MingStar.SimUniversity.AI.Evaluation
{
    /// <summary>
    /// Focus more on production
    /// </summary>
    public class ProductionGameScores : GameScores
    {
        public ProductionGameScores()
        {
            ProductionMultiplier = 200;
            StudentNumberMultiplier = 3;
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