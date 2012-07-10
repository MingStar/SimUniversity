using System.Collections.ObjectModel;
using MingStar.SimUniversity.Contract;
using MingStar.SimUniversity.Game.Random;

namespace MingStar.SimUniversity.Game.Move
{
    public class RandomMove : IPlayerMoveForUpdate
    {
        public IPlayerMoveForUpdate _actualMove;

        #region IPlayerMoveForUpdate Members

        public StudentGroup[] StudentsNeeded
        {
            get { return _actualMove.StudentsNeeded; }
        }

        public void ApplyTo(Game game)
        {
            GenerateMove(game);
            _actualMove.ApplyTo(game);
        }

        public void Undo(Game game)
        {
            _actualMove.Undo(game);
        }

        public bool IsLegalToApply(Game game)
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

        private void GenerateMove(Game game)
        {
            ReadOnlyCollection<IPlayerMoveForUpdate> moves = game.GenerateAllMoves();
            int index = RandomGenerator.Next(moves.Count);
            _actualMove = moves[index];
        }
    }
}