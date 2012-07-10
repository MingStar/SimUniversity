using System.Collections.Generic;
using MingStar.SimUniversity.Contract;

namespace MingStar.SimUniversity.Game.Move
{
    public class EndTurn : IProbabilityPlayerMove, IPlayerMoveForUpdate
    {
        private EnrolmentInfo _enrolmentInfo;
        public int DiceTotal { get; private set; }

        #region Constructors

        public EndTurn()
        {
            IsDeterminated = false;
            Probability = null;
        }

        private EndTurn(int total)
        {
            SetDiceTotal(total);
        }

        private void SetDiceTotal(int total)
        {
            DiceTotal = total;
            IsDeterminated = true;
            Probability = GameConstants.HexID2Chance[total]/
                          GameConstants.Chance.TotalDiceRoll;
        }

        #endregion

        #region IPlayerMoveForUpdate Members

        public void ApplyTo(Game game)
        {
            game.NextTurn();
            if (game.CurrentPhase == GamePhase.Play)
            {
                if (!IsDeterminated)
                {
                    SetDiceTotal(Game.RandomEventChance.GetNextDiceTotal());
                }
                _enrolmentInfo = game.DiceRoll(DiceTotal);
            }
        }

        public void Undo(Game game)
        {
            game.UndoEndTurn(DiceTotal, _enrolmentInfo);
        }


        public bool IsLegalToApply(Game game)
        {
            return game.CurrentPhase == GamePhase.Play;
        }

        #endregion

        #region IProbabilityPlayerMove Members

        public bool IsDeterminated { get; set; }
        public double? Probability { get; private set; }

        public StudentGroup[] StudentsNeeded
        {
            get { return null; }
        }

        public IEnumerable<IProbabilityPlayerMove> AllProbabilityMoves
        {
            get
            {
                return new[]
                           {
                               new EndTurn(2),
                               new EndTurn(3),
                               new EndTurn(4),
                               new EndTurn(5),
                               new EndTurn(6),
                               new EndTurn(7),
                               new EndTurn(8),
                               new EndTurn(9),
                               new EndTurn(10),
                               new EndTurn(11),
                               new EndTurn(12)
                           };
            }
        }

        #endregion

        public override string ToString()
        {
            string str = "End Turn";
            if (IsDeterminated)
            {
                str += " with dice roll: " + DiceTotal.ToString();
            }
            return str;
        }
    }
}