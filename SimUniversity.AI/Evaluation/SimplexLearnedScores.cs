using System;
using System.IO;
using System.Linq;
using MingStar.SimUniversity.AI.Learning;
using MingStar.SimUniversity.Contract;
using MingStar.Utilities.Persistence;

namespace MingStar.SimUniversity.AI.Evaluation
{
    [Serializable]
    public class SimplexLearnedScores : GameScores
    {
        private readonly Random _randomGenerator = new Random();

        public void FromResult(double[] result)
        {
            int i = 0;
            DegreeModifier[DegreeType.Ore] = result[i];
            ++i;
            DegreeModifier[DegreeType.Brick] = result[i];
            ++i;
            DegreeModifier[DegreeType.Wood] = result[i];
            ++i;
            DegreeModifier[DegreeType.Sheep] = result[i];
            ++i;
            DegreeModifier[DegreeType.Grain] = result[i];

            SetupDegreeModifier[DegreeType.Ore] = result[i];
            ++i;
            SetupDegreeModifier[DegreeType.Brick] = result[i];
            ++i;
            SetupDegreeModifier[DegreeType.Wood] = result[i];
            ++i;
            SetupDegreeModifier[DegreeType.Sheep] = result[i];
            ++i;
            SetupDegreeModifier[DegreeType.Grain] = result[i];
            ++i;

            PlayerScoreMultiplier = result[i];
            ++i;
            ProductionMultiplier = result[i];
            ++i;
            StudentNumberMultiplier = result[i];
            ++i;
            FutureCampus = result[i];
            ++i;
            SpecialSiteMultiplier = result[i];
            ++i;
            NormalSite = result[i];
            ++i;
            InternetLinkMultiplier = result[i];
            ++i;
            HasAllDegrees = result[i];
            ++i;
            TakenOtherPlayerCampus = result[i];
            ++i;
            LeadMostScore = result[i];
        }

        public override string ToString()
        {
            return "To be implemented, ideally return name-value pairs";
        }

        public SimplexConstant[] ToSimplexConstants()
        {
            double[] values =
                {
                    DegreeModifier[DegreeType.Ore], //0
                    DegreeModifier[DegreeType.Brick],
                    DegreeModifier[DegreeType.Wood],
                    DegreeModifier[DegreeType.Sheep],
                    DegreeModifier[DegreeType.Grain],
                    SetupDegreeModifier[DegreeType.Ore], //4
                    SetupDegreeModifier[DegreeType.Brick],
                    SetupDegreeModifier[DegreeType.Wood],
                    SetupDegreeModifier[DegreeType.Sheep],
                    SetupDegreeModifier[DegreeType.Grain],
                    PlayerScoreMultiplier, // 9
                    ProductionMultiplier,
                    StudentNumberMultiplier,
                    FutureCampus,
                    SpecialSiteMultiplier,
                    NormalSite, // 14
                    InternetLinkMultiplier,
                    HasAllDegrees,
                    TakenOtherPlayerCampus,
                    LeadMostScore
                };
            return (from value in values
                    select new SimplexConstant(value, GetRandomPerturbation(value))
                   ).ToArray();
        }


        private double GetRandomPerturbation(double value)
        {
            // [0.0, 0.5) --> [-value, 0)
            // [0.5, 0.1) --> [0, value * 5)
            var baseValue = (_randomGenerator.NextDouble() * 2 - 1.0) * value;
            if (baseValue < 0)
                return baseValue;
            return baseValue*5;
        }

        public void Save(string fileName)
        {
            XmlDataStore<SimplexLearnedScores>.Serialize(fileName, this);
        }

        public static SimplexLearnedScores Load(string fileName)
        {
            try
            {
                return XmlDataStore<SimplexLearnedScores>.Deserialize(fileName);
            }
            catch (FileNotFoundException)
            {
                return new SimplexLearnedScores();
            }
        }
    }
}