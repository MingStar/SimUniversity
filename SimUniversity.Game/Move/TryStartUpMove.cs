using System.Collections.Generic;
using MingStar.SimUniversity.Contract;

namespace MingStar.SimUniversity.Game.Move
{
    public class TryStartUpMove : IProbabilityPlayerMove, IPlayerMoveForUpdate, ITryStartUpMove
    {
        #region Constructors

        public TryStartUpMove()
        {
            IsDeterminated = false;
            Probability = null;
        }

        private TryStartUpMove(bool isSuccessful)
        {
            _isSuccessful = isSuccessful;
            IsDeterminated = true;
            Probability = _isSuccessful ? 0.2 : 0.8;
        }

        #endregion

        public static readonly StudentGroup[] NeededStudents =
            StudentGroup.FromDegrees(DegreeType.Ore, DegreeType.Grain, DegreeType.Sheep);

        private bool _isSuccessful;

        #region IPlayerMoveForUpdate Members

        public void ApplyTo(Game game)
        {
            if (!IsDeterminated)
            {
                _isSuccessful = Game.RandomEventChance.IsNextStartUpSuccessful();
                IsDeterminated = true;
            }
            game.TryStartUp(_isSuccessful);
        }

        public void Undo(Game game)
        {
            game.UndoTryStartUp(_isSuccessful);
        }

        public bool IsLegalToApply(Game game)
        {
            return true;
        }

        #endregion

        #region IProbabilityPlayerMove Members

        public bool IsDeterminated { get; set; }
        public double? Probability { get; private set; }

        public StudentGroup[] StudentsNeeded
        {
            get { return NeededStudents; }
        }

        public IEnumerable<IProbabilityPlayerMove> AllProbabilityMoves
        {
            get
            {
                return new IProbabilityPlayerMove[]
                           {
                               new TryStartUpMove(true),
                               new TryStartUpMove(false)
                           };
            }
        }

        #endregion

        public override string ToString()
        {
            string str = "Try Start Up";
            if (IsDeterminated)
            {
                str += ", will " + (_isSuccessful ? "succeed" : "failed");
            }
            return str;
        }
    }
}