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

        public double PlayerScoreMultiplier { get; set; }
        public double ProductionMultiplier { get; set; }
        public double SpecialSiteMultiplier { get; set; }
        public double StudentNumberMultiplier { get; set; }
        public XmlSerializableDictionary<DegreeType, double> SetupDegreeModifier { get; set; }
        public double TakenOtherPlayerCampus { get; set; }

        private string _name;

        public virtual string Name
        {
            get
            {
                if (_name == null)
                {
                    _name = GetType().Name;
                }
                return _name;
            }
            set { _name = value; }
        }

        public GameScores()
        {
            PlayerScoreMultiplier = 80.0;
            ProductionMultiplier = 100.0;
            StudentNumberMultiplier = 16.0;
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