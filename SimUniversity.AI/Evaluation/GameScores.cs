using System;
using System.Text;
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

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.Append(Name);
            builder.Append(" [");            
            builder.Append("Degree: (");
            builder.AppendFormat("Ore x {0:N2}, ", DegreeModifier[DegreeType.Ore]);
            builder.AppendFormat("Brick x {0:N2}, ", DegreeModifier[DegreeType.Brick]);
            builder.AppendFormat("Wood x {0:N2}, ", DegreeModifier[DegreeType.Wood]);
            builder.AppendFormat("Grain x {0:N2}, ", DegreeModifier[DegreeType.Grain]);
            builder.AppendFormat("Sheep x {0:N2}", DegreeModifier[DegreeType.Sheep]);
            builder.Append("), ");
            builder.Append("Setup Phase: (");
            builder.AppendFormat("Ore x {0:N2}, ", SetupDegreeModifier[DegreeType.Ore]);
            builder.AppendFormat("Brick x {0:N2}, ", SetupDegreeModifier[DegreeType.Brick]);
            builder.AppendFormat("Wood x {0:N2}, ", SetupDegreeModifier[DegreeType.Wood]);
            builder.AppendFormat("Grain x {0:N2}, ", SetupDegreeModifier[DegreeType.Grain]);
            builder.AppendFormat("Sheep x {0:N2}", SetupDegreeModifier[DegreeType.Sheep]);
            builder.Append("), ");
            builder.AppendFormat("Score x {0:N2}, ", PlayerScoreMultiplier);
            builder.AppendFormat("Production# x {0:N2}, ", ProductionMultiplier);
            builder.AppendFormat("Student# x {0:N2}, ", StudentNumberMultiplier);
            builder.AppendFormat("Future Campus# x {0:N2}, ", FutureCampus);
            builder.AppendFormat("Special Site x {0:N2}, ", SpecialSiteMultiplier);
            builder.AppendFormat("Internet Link# x {0:N2}, ", InternetLinkMultiplier);
            builder.AppendFormat("Taken Other Player Campus x {0:N2}, ", TakenOtherPlayerCampus);
            builder.AppendFormat("Normal Site + {0:N2}, ", NormalSite);
            builder.AppendFormat("All Degress + {0:N2}, ", HasAllDegrees);
            builder.AppendFormat("Lead Longest/Most Failed + {0:N2}", LeadMostScore);            
            builder.Append("]");
            return builder.ToString();
        }
    }
}