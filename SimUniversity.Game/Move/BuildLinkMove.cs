using MingStar.SimUniversity.Contract;

namespace MingStar.SimUniversity.Game.Move
{
    public class BuildLinkMove : IPlayerMove
    {
        public static readonly StudentGroup[] NeededStudents =
            StudentGroup.FromDegrees(DegreeType.Wood, DegreeType.Brick);

        public BuildLinkMove(EdgePosition where)
        {
            WhereAt = where;
        }

        public EdgePosition WhereAt { get; private set; }

        #region IPlayerMove Members

        public StudentGroup[] StudentsNeeded
        {
            get { return NeededStudents; }
        }

        public void ApplyTo(IGame game)
        {
            game.BuildLink(WhereAt);
        }

        public void Undo(IGame game)
        {
            game.UndoBuildLink(WhereAt);
        }

        public bool IsLegalToApply(IGame game)
        {
            return game.IsLegalToBuildLink(WhereAt);
        }

        #endregion

        public override string ToString()
        {
            return string.Format("Build internet link at [{0}]", WhereAt);
        }
    }
}