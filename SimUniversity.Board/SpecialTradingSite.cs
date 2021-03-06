﻿using MingStar.SimUniversity.Contract;

namespace MingStar.SimUniversity.Board
{
    public class SpecialTradingSite : ISpecialTradingSite
    {
        public const int TradeOutStudentQuantity = 2;
        private readonly StudentGroup _studentsNeeded;

        public SpecialTradingSite(DegreeType degree)
        {
            TradeOutDegree = degree;
            _studentsNeeded =
                new StudentGroup(degree, TradeOutStudentQuantity);
        }

        #region ISpecialTradingSite Members

        public DegreeType TradeOutDegree { get; private set; }

        public StudentGroup StudentsNeeded
        {
            get { return _studentsNeeded; }
        }

        #endregion

        public override string ToString()
        {
            return "Special Site: " + TradeOutDegree;
        }
    }
}