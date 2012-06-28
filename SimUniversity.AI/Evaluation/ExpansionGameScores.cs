using MingStar.SimUniversity.Contract;

namespace MingStar.SimUniversity.AI.Evaluation
{
    public class ExpansionGameScores : GameScores
    {
        public ExpansionGameScores()
        {
            PLAYER_SCORE_BASE = 150;
            PRODUCTION_BASE = 100;
            STUDENT_NUMBER = 3;
            FUTURE_CAMPUS = 30;
            SPECIAL_SITE_MULTIPLIER = 10;
            NORMAL_SITE = 200;
            INTERNET_LINK_MULTIPLIER = 5;
            HAS_ALL_DEGREES = 100;
            TAKEN_OTHER_PLAYER_CAMPUS = 100;
            LEAD_MOST_SCORE = 200;

            DegreeModifier[DegreeType.Brick] = 1.3;
            DegreeModifier[DegreeType.Wood] = 1.3;
        }
    }
}