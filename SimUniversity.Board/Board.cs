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

        #region Board Consturction Related

        public int MinX { get; private set; }
        public int MaxX { get; private set; }
        public int MinY { get; private set; }
        public int MaxY { get; private set; }

        internal void GetLimits(Position pos)
        {
            MinX = Math.Min(pos.X, MinX);
            MaxX = Math.Max(pos.X, MaxX);
            MinY = Math.Min(pos.Y, MinY);
            MaxY = Math.Max(pos.Y, MaxY);
        }

        private Hexagon GetHexagonOrNull(Position position)
        {
            Hexagon hex = null;
            _hexgonPositions.TryGetValue(position, out hex);
            return hex;
        }

        internal Hexagon CreateHexagon(int id, DegreeType degree, Position position)
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
            get { return _id2HexgonMap.ContainsKey(id) ? _id2HexgonMap[id].ToArray() : new Hexagon[] {}; }
        }

        public bool IsLocked { get; internal set; }

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
                Hexagon hex = GetHexagonOrNull(pos.HexPosition);
                return hex != null ? hex[pos.Orientation] : null;
            }
        }

        public Vertex this[VertexPosition pos]
        {
            get
            {
                Hexagon hex = GetHexagonOrNull(pos.HexPosition);
                return hex != null ? hex[pos.Orientation] : null;
            }
        }

        public Hexagon[] GetHexagons()
        {
            return _hexagons.ToArray();
        }

        public IEnumerable<Vertex> GetVertices()
        {
            if (_vertices == null)
            {
                var vertices = new HashSet<Vertex>();
                foreach (Hexagon hex in _hexagons)
                {
                    foreach (Vertex v in hex.Adjacent.Vertices)
                    {
                        vertices.Add(v);
                    }
                }
                _vertices = vertices.ToList().AsReadOnly();
            }
            return _vertices;
        }

        public IEnumerable<Edge> GetEdges()
        {
            if (_edges == null)
            {
                var edges = new HashSet<Edge>();
                foreach (Hexagon hex in _hexagons)
                {
                    foreach (Edge e in hex.Adjacent.Edges)
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
            foreach (Vertex vertex in GetVertices())
            {
                vertex.Reset();
            }
            foreach (Edge edge in GetEdges())
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