using System;
using MingStar.SimUniversity.Contract;
using MingStar.Utilities.Generics;

namespace MingStar.SimUniversity.AI.Evaluation
{
    [Serializable]
    public class GameScores
    {
        public XmlSerializableDictionary<DegreeType, double> DegreeModifier;
        public double FUTURE_CAMPUS;
        public double HAS_ALL_DEGREES;
        public double INTERNET_LINK_MULTIPLIER;
        public double LEAD_MOST_SCORE;
        public double NORMAL_SITE;

        public double PLAYER_SCORE_BASE;
        public double PRODUCTION_BASE;
        public double SPECIAL_SITE_MULTIPLIER;
        public double STUDENT_NUMBER;
        public XmlSerializableDictionary<DegreeType, double> SetupDegreeModifier;
        public double TAKEN_OTHER_PLAYER_CAMPUS;

        public GameScores()
        {
            PLAYER_SCORE_BASE = 80.0;
            PRODUCTION_BASE = 100.0;
            STUDENT_NUMBER = 16.0; // 3.0;
            FUTURE_CAMPUS = 30.0;
            SPECIAL_SITE_MULTIPLIER = 10.0;
            NORMAL_SITE = 200.0;
            INTERNET_LINK_MULTIPLIER = 10.0;
            HAS_ALL_DEGREES = 100.0;
            TAKEN_OTHER_PLAYER_CAMPUS = 100.0;
            LEAD_MOST_SCORE = 100.0;

            DegreeModifier = new XmlSerializableDictionary<DegreeType, double>();
            DegreeModifier[DegreeType.Ore] = 1.4;
            DegreeModifier[DegreeType.Brick] = 1.2;
            DegreeModifier[DegreeType.Wood] = 1.2;
            DegreeModifier[DegreeType.Sheep] = 1;
            DegreeModifier[DegreeType.Grain] = 1.1;
            DegreeModifier[DegreeType.None] = 0;

            SetupDegreeModifier = new XmlSerializableDictionary<DegreeType, double>();
            SetupDegreeModifier[DegreeType.Ore] = 1.3;
            SetupDegreeModifier[DegreeType.Brick] = 1.3;
            SetupDegreeModifier[DegreeType.Wood] = 1.3;
            SetupDegreeModifier[DegreeType.Sheep] = 1;
            SetupDegreeModifier[DegreeType.Grain] = 1.1;
            SetupDegreeModifier[DegreeType.None] = 0;
        }
    }
}