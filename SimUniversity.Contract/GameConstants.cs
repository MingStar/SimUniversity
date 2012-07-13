using System.Collections.Generic;

namespace MingStar.SimUniversity.Contract
{
    public static class GameConstants
    {
        public const int MaxNumberOfStudents = 7;

        public const int NumberOfInitialSetups = 2;

        public const int NormalTradingStudentQuantity = 4;

        public static readonly Dictionary<int, int> DiceRollNumber2Chance = new Dictionary<int, int>();


        static GameConstants()
        {
            DiceRollNumber2Chance[0] = 0; // desert
            DiceRollNumber2Chance[2] = 1;
            DiceRollNumber2Chance[3] = 2;
            DiceRollNumber2Chance[4] = 3;
            DiceRollNumber2Chance[5] = 4;
            DiceRollNumber2Chance[6] = 5;
            DiceRollNumber2Chance[7] = 6;
            DiceRollNumber2Chance[8] = 5;
            DiceRollNumber2Chance[9] = 4;
            DiceRollNumber2Chance[10] = 3;
            DiceRollNumber2Chance[11] = 2;
            DiceRollNumber2Chance[12] = 1;
        }

        public static readonly DegreeType[] RealDegrees = 
            new[]
            {
                DegreeType.Wood, 
                DegreeType.Brick, 
                DegreeType.Ore,
                DegreeType.Grain, 
                DegreeType.Sheep
            };

        #region Nested type: Chance

        public static class Chance
        {
            public const int SingleHexagonMax = 5;
            public const int SingleHexagonMin = 1;
            public const double TotalDiceRoll = 36.0;
        }

        #endregion

        #region Nested type: Score

        public static class Score
        {
            public const int MostFailedStartUps = 2;
            public const int LongestInternetLinks = 2;
        }

        #endregion
    }
}