namespace MingStar.SimUniversity.Contract
{
    public interface IPlayerMove
    {
        StudentGroup[] StudentsNeeded { get; }
        bool IsLegalToApply(IGame game);
        void ApplyTo(IGame game);
        void Undo(IGame game);
    }
}