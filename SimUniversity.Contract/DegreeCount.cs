﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MingStar.SimUniversity.Contract
{
    public class DegreeCount : Dictionary<DegreeType, int>
    {
        public DegreeCount()
        {
            foreach (var degree in GameConstants.RealDegrees)
            {
                base[degree] = 0;
            }
        }
        
        public int this[DegreeType degree]
        {
            get 
            {
                if (degree == DegreeType.None)
                {
                    return 0;
                }
                return base[degree]; 
            }
            set
            {
                if (degree != DegreeType.None)
                {
                    base[degree] = value;
                }
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

        public override bool Equals(object obj)
        {
            var other = obj as DegreeCount;
            if (other == null || Keys.Count != other.Keys.Count)
            {
                return false;
            }
            return Keys.All(key => other.ContainsKey(key) && this[key] == other[key]);
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.Append("[");
            foreach (var degree in GameConstants.RealDegrees)
            {
                if (this[degree] > 0)
                {
                    builder.AppendFormat("{0} x {1}, ", degree, this[degree]);
                }
            }
            builder.Append("]");
            return builder.ToString();
        }
    }
}