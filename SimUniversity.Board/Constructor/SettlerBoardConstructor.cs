using System.Collections.Generic;
using MingStar.SimUniversity.Contract;
using MingStar.Utilities.Linq;

namespace MingStar.SimUniversity.Board.Constructor
{
    public class SettlerBoardConstructor : BoardConstructor, IPredefinedBoardConstructor
    {
        private static readonly DegreeCount _originalList;

        static SettlerBoardConstructor()
        {
            _originalList = new DegreeCount();
            _originalList[DegreeType.Grain] = 4;
            _originalList[DegreeType.Wood] = 4;
            _originalList[DegreeType.Brick] = 3;
            _originalList[DegreeType.Ore] = 3;
            _originalList[DegreeType.Sheep] = 4;
        }

        #region IPredefinedBoardConstructor Members
        IBoard IPredefinedBoardConstructor.ConstructBoard()
        {
            return ConstructBoard();
        }
        #endregion

        public Board ConstructBoard()
        {
            Board = new Board();
            var degrees = GetDegrees();
            bool isFirst = true;
            var tokenEnumerator = GetTokens().GetEnumerator();
            var placeOrientationEnumberator = GetTokenPlaceOrders().GetEnumerator();
            foreach (var degree in degrees)
            {
                int token;
                DegreeType degreeToUse;
                if (degree == DegreeType.None)
                {
                    token = 0;
                    degreeToUse = DegreeType.None;
                }
                else
                {
                    tokenEnumerator.MoveNext();
                    token = tokenEnumerator.Current;
                    degreeToUse = degree;
                }
                if (isFirst)
                {
                    PlaceFirstHexagon(token, degreeToUse);
                    isFirst = false;
                }
                else
                {
                    placeOrientationEnumberator.MoveNext();
                    PlaceNextHexagon(token, degreeToUse,
                                     placeOrientationEnumberator.Current);
                }
            }
            PlaceHexagonsEnd();
            // set sites
            SetNormalSites(0, 0, VertexOrientation.BottomLeft, VertexOrientation.BottomRight);
            SetNormalSites(-2, 4, VertexOrientation.TopLeft, VertexOrientation.Left);
            SetNormalSites(1, 3, VertexOrientation.TopLeft, VertexOrientation.TopRight);
            SetNormalSites(2, 2, VertexOrientation.TopRight, VertexOrientation.Right);

            var siteDegreeEnumerator = GetSpecialSiteDegrees().GetEnumerator();
            siteDegreeEnumerator.MoveNext();
            SetSpecializedSites(-1, 1, VertexOrientation.Left, VertexOrientation.BottomLeft,
                                siteDegreeEnumerator.Current);
            siteDegreeEnumerator.MoveNext();
            SetSpecializedSites(-2, 3, VertexOrientation.Left, VertexOrientation.BottomLeft,
                                siteDegreeEnumerator.Current);
            siteDegreeEnumerator.MoveNext();
            SetSpecializedSites(-1, 4, VertexOrientation.TopLeft, VertexOrientation.TopRight,
                                siteDegreeEnumerator.Current);
            siteDegreeEnumerator.MoveNext();
            SetSpecializedSites(2, 1, VertexOrientation.Right, VertexOrientation.BottomRight,
                                siteDegreeEnumerator.Current);
            siteDegreeEnumerator.MoveNext();
            SetSpecializedSites(1, 0, VertexOrientation.Right, VertexOrientation.BottomRight,
                                siteDegreeEnumerator.Current);
            Lock();
            return Board;
        }


        public virtual IEnumerable<DegreeType> GetDegrees()
        {
            var list = _originalList.ToList();
            list.Add(DegreeType.None);
            return list.Shuffle();
        }

        public virtual IEnumerable<DegreeType> GetSpecialSiteDegrees()
        {
            return GameConstants.RealDegrees.Shuffle();
        }

        public virtual IEnumerable<int> GetTokens()
        {
            return new[] {5, 2, 6, 3, 8, 10, 9, 12, 11, 4, 8, 10, 9, 4, 5, 6, 3, 11};
        }

        public virtual IEnumerable<EdgeOrientation> GetTokenPlaceOrders()
        {
            return new[]
                       {
                           EdgeOrientation.TopRight,
                           EdgeOrientation.TopRight,
                           EdgeOrientation.Top,
                           EdgeOrientation.Top,
                           EdgeOrientation.TopLeft,
                           EdgeOrientation.TopLeft,
                           EdgeOrientation.BottomLeft,
                           EdgeOrientation.BottomLeft,
                           EdgeOrientation.Bottom,
                           EdgeOrientation.Bottom,
                           EdgeOrientation.BottomRight,
                           EdgeOrientation.TopRight,
                           EdgeOrientation.TopRight,
                           EdgeOrientation.Top,
                           EdgeOrientation.TopLeft,
                           EdgeOrientation.BottomLeft,
                           EdgeOrientation.Bottom,
                           EdgeOrientation.TopRight
                       };
        }
    }
}