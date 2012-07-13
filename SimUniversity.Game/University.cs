using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MingStar.SimUniversity.Board;
using MingStar.SimUniversity.Contract;
using MingStar.Utilities.Linq;

namespace MingStar.SimUniversity.Game
{
    public class University : IUniversity
    {
        private readonly Game _game;
        private HashSet<IVertex> _normalSiteLocations;

        public University(Game game, Color color, int playerIndex)
        {
            _game = game;
            Color = color;
            PlayerIndex = playerIndex;
            Reset();
        }


        public readonly int PlayerIndex;

        #region IUniversity Members
        public Color Color { get; private set; }
        public HashSet<IEdge> InternetLinks { get; private set; }
        public HashSet<IVertex> Campuses { get; private set; }
        public HashSet<IVertex> SuperCampuses { get; private set; }
        public HashSet<ISpecialTradingSite> SpecialSites { get; private set; }
        public DegreeCount Students { get; private set; }
        public int NumberOfSuccessfulCompanies { get; internal set; }
        public int NumberOfFailedCompanies { get; internal set; }
        public int LengthOfLongestLink { get; internal set; }
        public DegreeCount ProductionChances { get; private set; }
        public bool HasNormalTradingSite
        {
            get { return _normalSiteLocations.Count > 0; }
        }

        public bool HasStudentsFor(params StudentGroup[] studentGroups)
        {
            if (studentGroups == null)
            {
                return true;
            }
            return !studentGroups.Any(group => Students[group.Degree] < group.Quantity);
        }
        #endregion

        private int _generalTradingRate;

        internal IEnumerable<StudentGroup> GetDegreeTradingRates()
        {
            return GameConstants.RealDegrees.Select(d =>
                new StudentGroup(d,
                    SpecialSites.Any(s => s.TradeOutDegree == d)
                    ? SpecialTradingSite.TradeOutStudentQuantity
                    : _generalTradingRate)
                );
        }

        internal void Reset()
        {
            InternetLinks = new HashSet<IEdge>();
            Campuses = new HashSet<IVertex>();
            SuperCampuses = new HashSet<IVertex>();
            SpecialSites = new HashSet<ISpecialTradingSite>();
            _normalSiteLocations = new HashSet<IVertex>();
            Students = new DegreeCount();
            ProductionChances = new DegreeCount();
            ResetStudentCounts();
            NumberOfSuccessfulCompanies = 0;
            NumberOfFailedCompanies = 0;
            _generalTradingRate = GameConstants.NormalTradingStudentQuantity;
        }

        private void ResetStudentCounts()
        {
            foreach (var degree in GameConstants.RealDegrees)
            {
                Students[degree] = 0;
            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("University " + Color);
            sb.Append(" Score: " + _game.GetScore(this));
            sb.Append(" [");
            bool isFirst = true;
            foreach (DegreeType degree in Students.Keys)
            {
                if (Students[degree] != 0)
                {
                    if (!isFirst)
                    {
                        sb.Append(", ");
                    }
                    sb.Append(degree + ": " + Students[degree]);
                    isFirst = false;
                }
            }
            sb.Append("] ");
            if (LengthOfLongestLink > 0)
            {
                sb.AppendFormat("Longest: {0} ", LengthOfLongestLink);
            }
            if (Campuses.Count > 0)
            {
                sb.AppendFormat("Campuses: {0} ", Campuses.Count);
            }
            if (SuperCampuses.Count > 0)
            {
                sb.AppendFormat("SuperCampuses: {0} ", SuperCampuses.Count);
            }
            if (NumberOfFailedCompanies > 0)
            {
                sb.AppendFormat("Failed: {0} ", NumberOfFailedCompanies);
            }
            if (NumberOfSuccessfulCompanies > 0)
            {
                sb.AppendFormat("Successful: {0} ", NumberOfSuccessfulCompanies);
            }
            return sb.ToString();
        }

        public void RemoveCampus(IVertex vertex)
        {
            if (vertex.Campus == null)
            {
                return;
            }
            RemoveProductionChances(vertex);
            if (vertex.Campus.Type == CampusType.Super)
            {
                SuperCampuses.Remove(vertex);
                Campuses.Add(vertex);
            }
            else //if (vertex.Campus.Type == CampusType.Traditional)
            {
                Campuses.Remove(vertex);
                CheckLongestLinkForOtherUniversities(vertex);
                RemoveTradingSite(vertex);
            }
        }

        private void RemoveTradingSite(IVertex vertex)
        {
            if (vertex.TradingSite != null)
            {
                if (vertex.TradingSite == TradingSite.Instance)
                {
                    _normalSiteLocations.Remove(vertex);
                    if (!HasNormalTradingSite)
                    {
                        _generalTradingRate = GameConstants.NormalTradingStudentQuantity;
                    }
                }
                else
                {
                    SpecialSites.Remove((SpecialTradingSite) vertex.TradingSite);
                }
            }
        }

        public void AddCampus(IVertex vertex, CampusType type)
        {
            AddProductionChances(vertex);
            if (type == CampusType.Traditional)
            {
                Campuses.Add(vertex);
                _game.Hashing.HashVertex(Color, vertex.Position, type);
                CheckLongestLinkForOtherUniversities(vertex);
                AddTradingSite(vertex);
            }
            else //if (type == CampusType.Super)
            {
                SuperCampuses.Add(vertex);
                Campuses.Remove(vertex);
                _game.Hashing.HashVertex(Color, vertex.Position, CampusType.Super);
                _game.Hashing.HashVertex(Color, vertex.Position, CampusType.Traditional);
            }
        }

        private void AddTradingSite(IVertex vertex)
        {
            if (vertex.TradingSite != null)
            {
                if (vertex.TradingSite == TradingSite.Instance)
                {
                    _normalSiteLocations.Add(vertex);
                    _generalTradingRate = TradingSite.TradeOutStudentQuantity;
                }
                else
                {
                    SpecialSites.Add((SpecialTradingSite) vertex.TradingSite);
                }
            }
        }

        private void CheckLongestLinkForOtherUniversities(IVertex vertex)
        {
            var colorCount = new Dictionary<Color, int>();
            foreach (IEdge edge in vertex.Adjacent.Edges)
            {
                if (edge.Color != null)
                {
                    Color color = edge.Color.Value;
                    if (color == Color)
                    {
                        continue; // skip, if the same uni
                    }
                    if (!colorCount.ContainsKey(color))
                    {
                        colorCount[color] = 0;
                    }
                    colorCount[color]++;
                }
            }
            foreach (var count in colorCount)
            {
                if (count.Value > 1)
                {
                    _game.GetUniversityByColor(count.Key).FullSearchLongestLink();
                }
            }
        }

        /// <summary>
        /// Don't need to check campus type, 
        /// because each change (none -> tranditional OR tranditional -> super)
        /// will increase chances by 1 times only. (NOT 2 times)
        /// </summary>
        /// <param name="vertex"></param>
        private void AddProductionChances(IVertex vertex)
        {
            foreach (var hex in vertex.Adjacent.Hexagons)
            {
                ProductionChances[hex.Degree] += GameConstants.DiceRollNumber2Chance[hex.ProductionNumber];
            }
        }

        /// <summary>
        /// Don't need to check campus type, same reason as AddProductionChances()
        /// </summary>
        /// <param name="vertex"></param>
        private void RemoveProductionChances(IVertex vertex)
        {
            foreach (IHexagon hex in vertex.Adjacent.Hexagons)
            {
                ProductionChances[hex.Degree] -= GameConstants.DiceRollNumber2Chance[hex.ProductionNumber];
            }
        }

        internal void AddLink(IEdge edge)
        {
            InternetLinks.Add(edge);
            GetLongestLink(edge);
        }


        internal void RemoveLink(IEdge edge)
        {
            InternetLinks.Remove(edge);
        }

        private void GetLongestLink(IEdge edge)
        {
            int total = 0;
            var visitedEdges = new HashSet<IEdge>();
            if (edge.ConnectsBothEndWithSameColorEdges())
            {
                FullSearchLongestLink();
            }
            else // only connects on one side, but do not know which side
            {
                total += edge.Adjacent.Vertices.Sum(vertex => GetLongestLink(edge, vertex, visitedEdges));
                total -= 1; // the current edge is counted twice
                CompareLongestLink(total);
            }
        }


        private void CompareLongestLink(int count)
        {
            if (LengthOfLongestLink < count)
            {
                LengthOfLongestLink = count;
            }
        }

        private void FullSearchLongestLink()
        {
            LengthOfLongestLink = 0;
            foreach (var edge in InternetLinks)
            {
                foreach (var vertex in edge.Adjacent.Vertices)
                {
                    CompareLongestLink(GetLongestLink(edge, vertex, new HashSet<IEdge>()));
                }
            }
        }

        private int GetLongestLink(IEdge currentEdge, IVertex useVertex, HashSet<IEdge> visitedEdges)
        {
            if (visitedEdges.Contains(currentEdge))
            {
                return 0; // if already visited
            }
            if (useVertex.Campus != null && useVertex.Campus.Color != Color)
            {
                return 1; // if other player's campus is in the way
            }
            visitedEdges.Add(currentEdge);
            int max = 0;
            foreach (IEdge edge in currentEdge.GetAdjacentEdgesSharedWith(useVertex))
            {
                if (edge.Color != Color)
                    continue;
                // use the vertex on the other side of the edge
                int path = GetLongestLink(edge,
                                          edge.Adjacent.Vertices.First(v => v != useVertex),
                                          visitedEdges);
                if (max < path)
                {
                    max = path;
                }
            }
            visitedEdges.Remove(currentEdge);
            return max + 1;
        }

        internal DegreeType[] CutStudents()
        {
            if (Students.GetTotalCount() <= GameConstants.MaxNumberOfStudents)
            {
                return null;
            }
            HashAllDegrees();
            var suffled = Students.ToList().Shuffle();
            ResetStudentCounts();
            foreach (var degree in suffled.Take(GameConstants.MaxNumberOfStudents))
            {
                ++Students[degree];
            }
            HashAllDegrees();
            return suffled.Skip(GameConstants.MaxNumberOfStudents).ToArray();
        }

        private void HashAllDegrees()
        {
            foreach (DegreeType degree in GameConstants.RealDegrees)
            {
                _game.Hashing.HashDegree(Color, degree, Students[degree]);
            }
        }

        internal void ConsumeStudents(IPlayerMove move)
        {
            if (move.StudentsNeeded == null)
            {
                return;
            }
            foreach (StudentGroup group in move.StudentsNeeded)
            {
                _game.Hashing.HashDegree(Color, group.Degree, Students[group.Degree]);
                Students[group.Degree] -= group.Quantity;
                _game.Hashing.HashDegree(Color, group.Degree, Students[group.Degree]);
            }
        }

        internal void ResumeStudents(IPlayerMove move)
        {
            if (move.StudentsNeeded == null)
            {
                return;
            }
            foreach (StudentGroup group in move.StudentsNeeded)
            {
                Students[group.Degree] += group.Quantity;
            }
        }

        internal void RemoveExtraStudents(DegreeCount roll)
        {
            if (roll == null) return;
            foreach (DegreeType degree in roll.Keys)
            {
                Students[degree] -= roll[degree];
            }
        }

        internal void UntradeInStudents(DegreeType tradedIn)
        {
            Students[tradedIn] -= 1;
        }


        internal void AddBackStudents(DegreeType[] degreeType)
        {
            if (degreeType == null) return;
            foreach (var degree in degreeType)
            {
                Students[degree] += 1;
            }
        }

        internal void EnrolStudents(DegreeType degree, int number)
        {
            _game.Hashing.HashDegree(Color, degree, Students[degree]);
            Students[degree] += number;
            _game.Hashing.HashDegree(Color, degree, Students[degree]);
        }

        internal void AcquireInitialStudents(VertexPosition vertexPosition)
        {
            IEnumerable<DegreeType> degrees = _game.Board[vertexPosition].Adjacent.Hexagons.Select(h => h.Degree);
            foreach (DegreeType degree in degrees)
            {
                if (degree != DegreeType.None)
                {
                    Students[degree] += 1;
                }
            }
        }
    }
}