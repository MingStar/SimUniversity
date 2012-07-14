using System;
using System.Linq;
using MingStar.SimUniversity.AI.Learning;
using MingStar.SimUniversity.Contract;
using MingStar.Utilities.Persistence;

namespace MingStar.SimUniversity.AI.Evaluation
{
    [Serializable]
    public class SimplexScoresOnSetup : GameScores
    {
        private readonly Random _randomGenerator = new Random();

        public void FromResult(double[] result)
        {
            int i = 0;
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
            HasAllDegrees = result[i];
        }

        public SimplexConstant[] ToSimplexConstants()
        {
            double[] values =
                {
                    SetupDegreeModifier[DegreeType.Ore],
                    SetupDegreeModifier[DegreeType.Brick],
                    SetupDegreeModifier[DegreeType.Wood],
                    SetupDegreeModifier[DegreeType.Sheep],
                    SetupDegreeModifier[DegreeType.Grain],
                    HasAllDegrees,
                };
            return (from value in values
                    select new SimplexConstant(value, GetRandomPerturbation(value))
                   ).ToArray();
        }


        private double GetRandomPerturbation(double value)
        {
            // [0.0, 1) --> [-value, value)
            return (_randomGenerator.NextDouble()*2 - 1.0)*value*100;
        }

        public void Save(string fileName)
        {
            XmlDataStore<SimplexScoresOnSetup>.Serialize(fileName, this);
        }

        public static SimplexScoresOnSetup Load(string fileName)
        {
            return XmlDataStore<SimplexScoresOnSetup>.Deserialize(fileName);
        }
    }
}