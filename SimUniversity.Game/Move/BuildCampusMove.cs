using MingStar.SimUniversity.Contract;

namespace MingStar.SimUniversity.Game.Move
{
    public class BuildCampusMove : IPlayerMoveForUpdate, IBuildCampusMove
    {
        public static readonly StudentGroup[] StudentsNeededForTraditionalCampus =
            StudentGroup.FromDegrees(DegreeType.Wood, DegreeType.Brick, DegreeType.Grain, DegreeType.Sheep);

        public static readonly StudentGroup[] StudentsNeededForSuperCampus = new[]
                                                                                 {
                                                                                     new StudentGroup(DegreeType.Ore, 3)
                                                                                     ,
                                                                                     new StudentGroup(DegreeType.Grain,
                                                                                                      2)
                                                                                 };

        public BuildCampusMove(VertexPosition where, CampusType type)
        {
            WhereAt = where;
            CampusType = type;
        }

        public VertexPosition WhereAt { get; private set; }
        public CampusType CampusType { get; private set; }

        #region IPlayerMoveForUpdate Members

        public StudentGroup[] StudentsNeeded
        {
            get
            {
                if (CampusType == CampusType.Traditional)
                {
                    return StudentsNeededForTraditionalCampus;
                }
                return StudentsNeededForSuperCampus;
            }
        }

        public void ApplyTo(Game game)
        {
            game.BuildCampus(WhereAt, CampusType);
        }

        public void Undo(Game game)
        {
            game.UndoBuildCampus(WhereAt);
        }

        public bool IsLegalToApply(Game game)
        {
            return game.IsLegalToBuildCampus(WhereAt, CampusType);
        }

        #endregion

        public override string ToString()
        {
            return string.Format("Build {0} campus at [{1}]", CampusType, WhereAt);
        }
    }
}