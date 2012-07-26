using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MingStar.SimUniversity.Contract;

namespace SimUniversity.GameEngine.Contract
{
    public class GameDto
    {
        public BoardDto Board { get; set; }
        public int CurrentUniversityIndex { get; set; }
        public UniversityDto[] Universities { get; set; }
        public int CurrentTurn { get; set; }
        public GamePhase CurrentPhase { get; set; }
        public bool HasWinner { get; set; }
        public MostInfoDto MostFailedStartUps { get; set; }
        public MostInfoDto LongestInternetLink { get; set; }
        public GameRulesDto Rules { get; set; }
    }
}
