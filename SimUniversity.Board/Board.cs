using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using MingStar.SimUniversity.Contract;

namespace MingStar.SimUniversity.Board
{
    public class Board
    {
        protected ReadOnlyCollection<Edge> _edges;
        protected HashSet<Hexagon> _hexagons = new HashSet<Hexagon>();
        protected Dictionary<Position, Hexagon> _hexgonPositions = new Dictionary<Position, Hexagon>();
        protected Dictionary<int, List<Hexagon>> _id2HexgonMap = new Dictionary<int, List<Hexagon>>();
        protected ReadOnlyCollection<Vertex> _vertices;
        private Hexagon m_lastPlacedHexagon;

        #region Board Consturction Related

        public int MinX { get; private set; }
        public int MaxX { get; private set; }
        public int MinY { get; private set; }
        public int MaxY { get; private set; }

        protected void PlaceFirstHexagon(int id, DegreeType degreeType)
        {
            Hexagon hex = CreateHexagon(id, degreeType, new Position(0, 0));
            m_lastPlacedHexagon = hex;
        }

        protected void PlaceFirst(int id, DegreeType degreeType, DegreeType siteDegreeType)
        {
            Hexagon hex = CreateHexagon(id, degreeType, new Position(0, 0));
            m_lastPlacedHexagon = hex;
        }

        protected void PlaceNextHexagon(int id, DegreeType degreeType, EdgeOrientation eo)
        {
            Position pos = m_lastPlacedHexagon.GetPositionNextTo(eo);
            GetLimits(pos);
            Hexagon hex = CreateHexagon(id, degreeType, pos);
            m_lastPlacedHexagon = hex;
        }

        private void GetLimits(Position pos)
        {
            MinX = Math.Min(pos.X, MinX);
            MaxX = Math.Max(pos.X, MaxX);
            MinY = Math.Min(pos.Y, MinY);
            MaxY = Math.Max(pos.Y, MaxY);
        }


        protected void PlaceHexagonsEnd()
        {
            m_lastPlacedHexagon = null;
            foreach (Hexagon hex in _hexagons)
            {
                hex.PlaceEnd(this);
            }
        }

        private Hexagon GetHexagonOrNull(Position position)
        {
            Hexagon hex = null;
            _hexgonPositions.TryGetValue(position, out hex);
            return hex;
        }

        private Hexagon CreateHexagon(int id, DegreeType degree, Position position)
        {
            var hex = new Hexagon(id, degree, position);
            _hexagons.Add(hex);
            _hexgonPositions[position] = hex;
            if (!_id2HexgonMap.ContainsKey(id))
            {
                _id2HexgonMap[id] = new List<Hexagon>();
            }
            _id2HexgonMap[id].Add(hex);
            return hex;
        }

        #endregion

        public Hexagon[] this[int id]
        {
            get
            {
                return _id2HexgonMap.ContainsKey(id) ? _id2HexgonMap[id].ToArray() : new Hexagon[] {};
            }
        }

        public bool IsLocked { get; private set; }

        public Hexagon this[int x, int y]
        {
            get { return GetHexagonOrNull(new Position(x, y)); }
        }

        public Hexagon this[Position pos]
        {
            get { return GetHexagonOrNull(pos); }
        }

        public Edge this[EdgePosition pos]
        {
            get
            {
                var hex = GetHexagonOrNull(pos.HexPosition);
                return hex != null ? hex[pos.Orientation] : null;
            }
        }

        public Vertex this[VertexPosition pos]
        {
            get
            {
                var hex = GetHexagonOrNull(pos.HexPosition);
                return hex != null ? hex[pos.Orientation] : null;
            }
        }

        public Hexagon[] GetHexagons()
        {
            return _hexagons.ToArray();
        }

        public ReadOnlyCollection<Vertex> GetVertices()
        {
            if (_vertices == null)
            {
                var vertices = new HashSet<Vertex>();
                foreach (var hex in _hexagons)
                {
                    foreach (var v in hex.Adjacent.Vertices)
                    {
                        vertices.Add(v);
                    }
                }
                _vertices = vertices.ToList().AsReadOnly();
            }
            return _vertices;
        }

        public ReadOnlyCollection<Edge> GetEdges()
        {
            if (_edges == null)
            {
                var edges = new HashSet<Edge>();
                foreach (var hex in _hexagons)
                {
                    foreach (var e in hex.Adjacent.Edges)
                    {
                        edges.Add(e);
                    }
                }
                _edges = edges.ToList().AsReadOnly();
            }
            return _edges;
        }

        public void Clear()
        {
            foreach (var vertex in GetVertices())
            {
                vertex.Reset();
            }
            foreach (var edge in GetEdges())
            {
                edge.Reset();
            }
        }

        public void BuildCampus(Vertex vertex, CampusType type, Color color)
        {
            vertex.BuildCampus(type, color);
        }

        public void BuildLink(Edge side, Color color)
        {
            side.Color = color;
        }

        protected void Lock()
        {
            IsLocked = true;
            FindAllAdjacents();
        }

        private void FindAllAdjacents()
        {
            foreach (var vertex in GetVertices())
            {
                vertex.FindAdjacents();
            }
            ReadOnlyCollection<Edge> allEdges = GetEdges();
            foreach (var edge in allEdges)
            {
                edge.FindAllAdjacents(this);
            }
            foreach (var edge in allEdges)
            {
                edge.FindAdjacentSharedEdges();
            }
        }


        public void SetNormalSites(int x, int y, VertexOrientation vo, VertexOrientation vo2)
        {
            var hex = this[x, y];
            if (hex == null) 
                return;
            hex[vo].MakeMultiSite();
            hex[vo2].MakeMultiSite();
        }

        public void SetSpecializedSites(int x, int y,
                                        VertexOrientation vo, VertexOrientation vo2, DegreeType degree)
        {
            Hexagon hex = this[x, y];
            if (hex != null)
            {
                hex[vo].MakeSpecialSite(degree);
                hex[vo2].MakeSpecialSite(degree);
            }
        }

        public void UnBuildCampus(VertexPosition whereAt)
        {
            this[whereAt].DowngradeCampus();
        }

        public void UnBuildLink(EdgePosition whereAt)
        {
            this[whereAt].Reset();
        }
    }
}