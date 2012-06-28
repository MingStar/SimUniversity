using System.Collections.Generic;
using System.Linq;

namespace MingStar.SimUniversity.Contract
{
    public class DegreeCount : Dictionary<DegreeType, int>
    {
        public DegreeCount()
        {
            foreach (DegreeType degree in Constants.RealDegrees)
            {
                base[degree] = 0;
            }
        }

        public int GetTotalCount()
        {
            return Values.Sum();
        }

        public void Add(DegreeCount count)
        {
            foreach (DegreeType degree in count.Keys)
            {
                base[degree] += count[degree];
            }
        }

        public List<DegreeType> ToList()
        {
            var degrees = new List<DegreeType>();
            foreach (DegreeType degree in Keys)
            {
                for (int i = 0; i < this[degree]; ++i)
                {
                    degrees.Add(degree);
                }
            }
            return degrees;
        }
    }
}