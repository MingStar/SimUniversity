namespace MingStar.SimUniversity.AI.Evaluation
{
    public class ProductionGameScores : GameScores
    {
        public ProductionGameScores()
        {
            PRODUCTION_BASE = 200;
            STUDENT_NUMBER = 3;
            FUTURE_CAMPUS = 60;
            SPECIAL_SITE_MULTIPLIER = 20;
            NORMAL_SITE = 500;
            INTERNET_LINK_MULTIPLIER = 10;
            HAS_ALL_DEGREES = 100;
            TAKEN_OTHER_PLAYER_CAMPUS = 100;
            LEAD_MOST_SCORE = 200;
        }
    }
}