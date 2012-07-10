using System.Collections.Generic;
using System.Linq;

namespace MingStar.SimUniversity.Contract
{
    public class StudentGroup
    {
        public StudentGroup(DegreeType degree, int quantity)
        {
            Degree = degree;
            Quantity = quantity;
        }

        public DegreeType Degree { get; private set; }
        public int Quantity { get; private set; }

        public static StudentGroup[] FromDegrees(params DegreeType[] degrees)
        {
            return (from degree in degrees
                    select new StudentGroup(degree, 1)).ToArray();
        }
    }
}