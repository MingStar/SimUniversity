using System;
using MingStar.SimUniversity.Contract;
using MingStar.Utilities.Generics;

namespace MingStar.SimUniversity.AI.Evaluation
{
    [Serializable]
    public class GameScores
    {
        public XmlSerializableDictionary<DegreeType, double> DegreeModifier { get; protected set; }
        public double FutureCampus { get; protected set; }
        public double HasAllDegrees { get; protected set; }
        public double InternetLinkMultiplier { get; protected set; }
        public double LeadMostScore { get; protected set; }
        public double NormalSite { get; protected set; }

        public double PlayerScoreBase { get; protected set; }
        public double ProductionBase { get; protected set; }
        public double SpecialSiteMultiplier { get; protected set; }
        public double StudentNumber { get; protected set; }
        public XmlSerializableDictionary<DegreeType, double> SetupDegreeModifier { get; protected set; }
        public double TakenOtherPlayerCampus { get; protected set; }

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