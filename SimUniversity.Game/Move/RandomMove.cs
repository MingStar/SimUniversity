using System.Collections.ObjectModel;
using MingStar.SimUniversity.Contract;
using MingStar.SimUniversity.Game.Random;

namespace MingStar.SimUniversity.Game.Move
{
    public class RandomMove : IPlayerMove
    {
        public IPlayerMove _actualMove;

        #region IPlayerMove Members

        public StudentGroup[] StudentsNeeded
        {
            get { return _actualMove.StudentsNeeded; }
        }

        public void ApplyTo(IGame game)
        {
            GenerateMove(game);
            _actualMove.ApplyTo(game);
        }

        public void Undo(IGame game)
        {
            _actualMove.Undo(game);
        }

        public bool IsLegalToApply(IGame game)
        {
            return _actualMove.IsLegalToApply(game);
        }

        #endregion

        public override string ToString()
        {
            string str = "Random Move";
            if (_actualMove != null)
            {
                str += string.Format(", actual [{0}]", _actualMove);
            }
            return str;
        }

        public void GenerateMove(IGame game)
        {
            ReadOnlyCollection<IPlayerMove> moves = game.GenerateAllMoves();
            int index = RandomGenerator.Next(moves.Count);
            _actualMove = moves[index];
        }
    }
}