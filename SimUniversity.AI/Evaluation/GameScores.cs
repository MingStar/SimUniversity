using System;
using MingStar.SimUniversity.Contract;
using MingStar.Utilities.Generics;

namespace MingStar.SimUniversity.AI.Evaluation
{
    [Serializable]
    public class GameScores
    {
        public XmlSerializableDictionary<DegreeType, double> DegreeModifier { get; set; }
        public double FutureCampus { get; set; }
        public double HasAllDegrees { get; set; }
        public double InternetLinkMultiplier { get; set; }
        public double LeadMostScore { get; set; }
        public double NormalSite { get; set; }

        public double PlayerScoreBase { get; set; }
        public double ProductionBase { get; set; }
        public double SpecialSiteMultiplier { get; set; }
        public double StudentNumber { get; set; }
        public XmlSerializableDictionary<DegreeType, double> SetupDegreeModifier { get; set; }
        public double TakenOtherPlayerCampus { get; set; }

        public GameScores()
        {
            PlayerScoreBase = 80.0;
            ProductionBase = 100.0;
            StudentNumber = 16.0; // 3.0;
            FutureCampus = 30.0;
            SpecialSiteMultiplier = 10.0;
            NormalSite = 200.0;
            InternetLinkMultiplier = 10.0;
            HasAllDegrees = 100.0;
            TakenOtherPlayerCampus = 100.0;
            LeadMostScore = 100.0;

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