using System.Collections.Generic;

namespace MingStar.SimUniversity.Game
{
    public static class GameConstants
    {
        public const int MaxNumberOfStudents = 7;

        public const int NumberOfInitialSetups = 2;

        public const int NormalTradingStudentQuantity = 4;

        public static readonly Dictionary<int, int> HexID2Chance = new Dictionary<int, int>();


        static GameConstants()
        {
            HexID2Chance[0] = 0; // desert
            HexID2Chance[2] = 1;
            HexID2Chance[3] = 2;
            HexID2Chance[4] = 3;
            HexID2Chance[5] = 4;
            HexID2Chance[6] = 5;
            HexID2Chance[7] = 6;
            HexID2Chance[8] = 5;
            HexID2Chance[9] = 4;
            HexID2Chance[10] = 3;
            HexID2Chance[11] = 2;
            HexID2Chance[12] = 1;
        }

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