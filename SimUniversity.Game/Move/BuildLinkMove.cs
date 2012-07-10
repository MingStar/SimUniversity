using System;
using MingStar.SimUniversity.Contract;

namespace MingStar.SimUniversity.Game.Move
{
    public class BuildLinkMove : IPlayerMoveForUpdate, IBuildLinkMove
    {
        public static readonly StudentGroup[] NeededStudents =
            StudentGroup.FromDegrees(DegreeType.Wood, DegreeType.Brick);

        public BuildLinkMove(EdgePosition where)
        {
            WhereAt = where;
        }

        public EdgePosition WhereAt { get; private set; }

        #region IPlayerMoveForUpdate Members

        public StudentGroup[] StudentsNeeded
        {
            get { return NeededStudents; }
        }

        public void ApplyTo(Game game)
        {
            game.BuildLink(WhereAt);
        }

        public void Undo(Game game)
        {
            game.UndoBuildLink(WhereAt);
        }

        public bool IsLegalToApply(Game game)
        {
            return game.IsLegalToBuildLink(WhereAt);
        }

        #endregion

        public override string ToString()
        {
            return string.Format("Build internet link at [{0}]", WhereAt);
        }


        public void ApplyTo(IGame game)
        {
            throw new NotImplementedException();
        }

        public void Undo(IGame game)
        {
            throw new NotImplementedException();
        }
    }
}