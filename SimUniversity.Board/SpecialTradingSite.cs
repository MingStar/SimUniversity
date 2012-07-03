using MingStar.SimUniversity.Contract;

namespace MingStar.SimUniversity.Board
{
    public class SpecialTradingSite : ITradingSite
    {
        public new const int TradeOutStudentQuantity = 2;
        private readonly StudentGroup _studentsNeeded;

        public SpecialTradingSite(DegreeType degree)
        {
            TradeOutDegree = degree;
            _studentsNeeded =
                new StudentGroup(degree, TradeOutStudentQuantity);
        }

        public DegreeType TradeOutDegree { get; private set; }

        public StudentGroup StudentsNeeded
        {
            get { return _studentsNeeded; }
        }

        public override string ToString()
        {
            return "Special Site: " + TradeOutDegree;
        }
    }
}