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
        public readonly Color Color;
        public readonly int PlayerIndex;
        private readonly Game _game;

        public University(Game game, Color color, int playerIndex)
        {
            _game = game;
            Color = color;
            PlayerIndex = playerIndex;
            Reset();
        }

        public HashSet<Edge> InternetLinks { get; private set; }
        public HashSet<Vertex> Campuses { get; private set; }
        public HashSet<Vertex> SuperCampuses { get; private set; }
        public HashSet<SpecialTradingSite> SpecialSites { get; private set; }
        public HashSet<Vertex> NormalSiteLocations { get; private set; }
        public DegreeCount Students { get; private set; }
        public int NumberOfSuccessfulCompanies { get; internal set; }
        public int NumberOfFailedCompanies { get; internal set; }
        public int LengthOfLongestLink { get; internal set; }
        public DegreeCount ProductionChances { get; private set; }

        public bool HasNormalTradingSite
        {
            get { return NormalSiteLocations.Count > 0; }
        }

        public string Name
        {
            get { return "University " + Color; }
        }

        #region IUniversity Members

        public bool HasStudentsFor(params StudentGroup[] studentGroups)
        {
            if (studentGroups == null)
            {
                return true;
            }
            return !studentGroups.Any(group => Students[group.Degree] < group.Quantity);
        }

        #endregion

        internal void Reset()
        {
            InternetLinks = new HashSet<Edge>();
            Campuses = new HashSet<Vertex>();
            SuperCampuses = new HashSet<Vertex>();
            SpecialSites = new HashSet<SpecialTradingSite>();
            NormalSiteLocations = new HashSet<Vertex>();
            Students = new DegreeCount();
            ProductionChances = new DegreeCount();
            ResetStudentCounts();
            NumberOfSuccessfulCompanies = 0;
            NumberOfFailedCompanies = 0;
            //DiceRollProductionChances = new Dictionary<int, int>();
            //DiceRollProductionChances.InitialiseKeys(2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12);
        }

        private void ResetStudentCounts()
        {
            foreach (DegreeType degree in Constants.RealDegrees)
            {
                Students[degree] = 0;
            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append(Name);
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

        internal void RemoveCampus(Vertex vertex)
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

        private void RemoveTradingSite(Vertex vertex)
        {
            if (vertex.TradingSite != null)
            {
                if (vertex.TradingSite == TradingSite.Instance)
                {
                    NormalSiteLocations.Remove(vertex);
                }
                else
                {
                    SpecialSites.Remove((SpecialTradingSite) vertex.TradingSite);
                }
            }
        }

        internal void AddCampus(Vertex vertex, CampusType type)
        {
            AddProductionChances(vertex);
            if (type == CampusType.Traditional)
            {
                Campuses.Add(vertex);
                _game.Hashing.HashVertex(Color, vertex.Position, type);
                CheckLongestLinkForOtherUniversities(vertex);
                if (vertex.TradingSite != null)
                {
                    if (vertex.TradingSite == TradingSite.Instance)
                    {
                        NormalSiteLocations.Add(vertex);
                    }
                    else
                    {
                        SpecialSites.Add((SpecialTradingSite) vertex.TradingSite);
                    }
                }
            }
            else //if (type == CampusType.Super)
            {
                SuperCampuses.Add(vertex);
                Campuses.Remove(vertex);
                _game.Hashing.HashVertex(Color, vertex.Position, CampusType.Super);
                _game.Hashing.HashVertex(Color, vertex.Position, CampusType.Traditional);
            }
        }

        private void CheckLongestLinkForOtherUniversities(Vertex vertex)
        {
            var colorCount = new Dictionary<Color, int>();
            foreach (Edge edge in vertex.Adjacent.Edges)
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
        private void AddProductionChances(Vertex vertex)
        {
            foreach (Hexagon hex in vertex.Adjacent.Hexagons)
            {
                ProductionChances[hex.Degree] += GameConstants.HexID2Chance[hex.ID];
                //DiceRollProductionChances[hex.ID] += GameConstants.HexID2Chance[hex.ID];
            }
        }

        /// <summary>
        /// Don't need to check campus type, same reason as AddProductionChances()
        /// </summary>
        /// <param name="vertex"></param>
        private void RemoveProductionChances(Vertex vertex)
        {
            foreach (Hexagon hex in vertex.Adjacent.Hexagons)
            {
                ProductionChances[hex.Degree] -= GameConstants.HexID2Chance[hex.ID];
                //DiceRollProductionChances[hex.ID] -= GameConstants.HexID2Chance[hex.ID];
            }
        }

        internal void AddLink(Edge edge)
        {
            InternetLinks.Add(edge);
            GetLongestLink(edge);
        }


        internal void RemoveLink(Edge edge)
        {
            InternetLinks.Remove(edge);
        }

        private void GetLongestLink(Edge edge)
        {
            int total = 0;
            var visitedEdges = new HashSet<Edge>();
            if (edge.ConnectsBothEndWithSameColorEdges())
            {
                FullSearchLongestLink();
            }
            else // only connects on one side, but do not know which side
            {
                foreach (var vertex in edge.Adjacent.Vertices)
                {
                    total += GetLongestLink(edge, vertex, visitedEdges);
                }
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
            foreach (Edge edge in InternetLinks)
            {
                foreach (Vertex vertex in edge.Adjacent.Vertices)
                {
                    CompareLongestLink(GetLongestLink(edge, vertex, new HashSet<Edge>()));
                }
            }
        }

        private int GetLongestLink(Edge currentEdge, Vertex useVertex, HashSet<Edge> visitedEdges)
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
            foreach (var edge in currentEdge.GetAdjacentEdgesSharedWith(useVertex))
            {
                if (edge.Color == Color)
                {
                    // use the vertex on the other side of the edge
                    int path = GetLongestLink(edge,
                                              edge.Adjacent.Vertices.First(v => v != useVertex),
                                              visitedEdges);
                    if (max < path)
                    {
                        max = path;
                    }
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
            IEnumerable<DegreeType> suffled = Students.ToList().Shuffle();
            ResetStudentCounts();
            foreach (DegreeType degree in suffled.Take(GameConstants.MaxNumberOfStudents))
            {
                ++Students[degree];
            }
            HashAllDegrees();
            return suffled.Skip(GameConstants.MaxNumberOfStudents).ToArray();
        }

        private void HashAllDegrees()
        {
            foreach (DegreeType degree in Constants.RealDegrees)
            {
                _game.Hashing.HashDegree(Color, degree, Students[degree]);
            }
        }

        public bool HasStudentsFor(DegreeType degree, int quantity)
        {
            return Students[degree] >= quantity;
        }

        internal void ConsumeStudents(IPlayerMove move)
        {
            if (move.StudentsNeeded == null)
            {
                return;
            }
            foreach (var group in move.StudentsNeeded)
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
            if (roll != null)
            {
                foreach (DegreeType degree in roll.Keys)
                {
                    Students[degree] -= roll[degree];
                }
            }
        }

        internal void UntradeInStudents(DegreeType tradedIn)
        {
            Students[tradedIn] -= 1;
        }


        internal void AddBackStudents(DegreeType[] degreeType)
        {
            if (degreeType != null)
            {
                foreach (DegreeType degree in degreeType)
                {
                    Students[degree] += 1;
                }
            }
        }

        internal void EnrolStudents(DegreeType degree, int number)
        {
            _game.Hashing.HashDegree(Color, degree, Students[degree]);
            Students[degree] += number;
            _game.Hashing.HashDegree(Color, degree, Students[degree]);
        }
    }
}