using System;
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

            PLAYER_SCORE_BASE = result[i];
            ++i;
            PRODUCTION_BASE = result[i];
            ++i;
            STUDENT_NUMBER = result[i];
            ++i;
            FUTURE_CAMPUS = result[i];
            ++i;
            SPECIAL_SITE_MULTIPLIER = result[i];
            ++i;
            NORMAL_SITE = result[i];
            ++i;
            INTERNET_LINK_MULTIPLIER = result[i];
            ++i;
            HAS_ALL_DEGREES = result[i];
            ++i;
            TAKEN_OTHER_PLAYER_CAMPUS = result[i];
            ++i;
            LEAD_MOST_SCORE = result[i];
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
                    PLAYER_SCORE_BASE, // 9
                    PRODUCTION_BASE,
                    STUDENT_NUMBER,
                    FUTURE_CAMPUS,
                    SPECIAL_SITE_MULTIPLIER,
                    NORMAL_SITE, // 14
                    INTERNET_LINK_MULTIPLIER,
                    HAS_ALL_DEGREES,
                    TAKEN_OTHER_PLAYER_CAMPUS,
                    LEAD_MOST_SCORE,
                };
            return (from value in values
                    select new SimplexConstant(value, GetRandomPerturbation(value))
                   ).ToArray();
        }


        private double GetRandomPerturbation(double value)
        {
            // [0.0, 1) --> [-value, value)
            return (_randomGenerator.NextDouble()*2 - 1.0)*value;
        }

        public void Save(string fileName)
        {
            XmlDataStore<SimplexLearnedScores>.Serialize(fileName, this);
        }

        public static SimplexLearnedScores Load(string fileName)
        {
            return XmlDataStore<SimplexLearnedScores>.Deserialize(fileName);
        }
    }
}