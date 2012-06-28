using System.Collections.Generic;
using System.Linq;

namespace MingStar.SimUniversity.Contract
{
    public class StudentGroup
    {
        public StudentGroup(DegreeType degree)
        {
            Degree = degree;
            Quantity = 1;
        }

        public StudentGroup(DegreeType degree, int quantity)
        {
            Degree = degree;
            Quantity = quantity;
        }

        public DegreeType Degree { get; private set; }
        public int Quantity { get; private set; }

        public static StudentGroup[] FromDegrees(params DegreeType[] degrees)
        {
            IEnumerable<StudentGroup> result = from degree in degrees
                                               select new StudentGroup(degree);
            return result.ToArray();
        }
    }
}