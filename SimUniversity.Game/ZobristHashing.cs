using System;
using System.Collections.Generic;
using MingStar.SimUniversity.Board;
using MingStar.SimUniversity.Contract;

namespace MingStar.SimUniversity.Game
{
    public class ZobristHashing : IZobristHashing
    {
        private readonly Dictionary<Color, ColoredHashing> _coloredHashing
            = new Dictionary<Color, ColoredHashing>();

        private readonly Game _game;
        private readonly HashSet<UInt64> _hashes = new HashSet<UInt64>();
        private readonly System.Random _random = new System.Random(2009);

        public ZobristHashing(Game game)
        {
            _game = game;
            var board = game.Board;
            foreach (var uni in game.Universities)
            {
                if (!_coloredHashing.ContainsKey(uni.Color))
                {
                    _coloredHashing[uni.Color] = new ColoredHashing();
                }
                var coloredHash = _coloredHashing[uni.Color];
                foreach (DegreeType degree in Constants.RealDegrees)
                {
                    for (int i = 1; i <= 20; ++i)
                    {
                        coloredHash.DegreeHash[degree][i] = NextNewInt64();
                    }
                }
                foreach (var vertex in board.GetVertices())
                {
                    coloredHash.CampusHash[CampusType.Traditional][vertex] = NextNewInt64();
                    coloredHash.CampusHash[CampusType.Super][vertex] = NextNewInt64();
                }
                foreach (var edge in board.GetEdges())
                {
                    coloredHash.LinkHash[edge] = NextNewInt64();
                }
                for (int i = 1; i <= 30; ++i)
                {
                    coloredHash.StartUpHash[true][i] = NextNewInt64();
                    coloredHash.StartUpHash[false][i] = NextNewInt64();
                }
                coloredHash.MostInfoHash[MostInfoType.FailedStartUps] = NextNewInt64();
                coloredHash.MostInfoHash[MostInfoType.Links] = NextNewInt64();
                coloredHash.CurrentUniversityHash = NextNewInt64();
            }
        }

        #region IZobristHashing Members

        public ulong Hash { get; set; }


        public UInt64 NextNewInt64()
        {
            UInt64 hash;
            while (true)
            {
                var buffer = new byte[sizeof (Int64)];
                _random.NextBytes(buffer);
                hash = BitConverter.ToUInt64(buffer, 0);
                if (!_hashes.Contains(hash))
                {
                    _hashes.Add(hash);
                    break;
                }
            }
            return hash;
        }

        public void HashCurrentUniversity(Color color)
        {
            Hash ^= _coloredHashing[color].CurrentUniversityHash;
        }

        public void HashVertex(Color color, VertexPosition vPos, CampusType type)
        {
            Vertex vertex = _game.Board[vPos];
            Hash ^= _coloredHashing[color].CampusHash[type][vertex];
        }

        public void HashEdge(Color color, EdgePosition ePos)
        {
            Edge edge = _game.Board[ePos];
            Hash ^= _coloredHashing[color].LinkHash[edge];
        }

        public void HashDegree(Color color, DegreeType degree, int quantity)
        {
            if (quantity == 0)
            {
                return;
            }
            Dictionary<int, ulong> dict = _coloredHashing[color].DegreeHash[degree];
            if (!dict.ContainsKey(quantity))
            {
                dict[quantity] = NextNewInt64();
            }
            Hash ^= dict[quantity];
        }

        public void HashStartUp(Color color, bool isSuccessful, int quantity)
        {
            if (quantity == 0)
            {
                return;
            }
            Dictionary<int, ulong> dict = _coloredHashing[color].StartUpHash[isSuccessful];
            if (!dict.ContainsKey(quantity))
            {
                dict[quantity] = NextNewInt64();
            }
            Hash ^= dict[quantity];
        }

        public void HashMostInfo(University uni, MostInfoType type)
        {
            if (uni == null)
            {
                return;
            }
            Hash ^= _coloredHashing[uni.Color].MostInfoHash[type];
        }

        #endregion

        #region Nested type: ColoredHashing

        private class ColoredHashing
        {
            public readonly Dictionary<CampusType, Dictionary<Vertex, ulong>> CampusHash;
            public readonly Dictionary<DegreeType, Dictionary<int, ulong>> DegreeHash;
            public readonly Dictionary<Edge, ulong> LinkHash;
            public readonly Dictionary<MostInfoType, ulong> MostInfoHash;
            public readonly Dictionary<bool, Dictionary<int, ulong>> StartUpHash;
            public ulong CurrentUniversityHash;

            public ColoredHashing()
            {
                DegreeHash = new Dictionary<DegreeType, Dictionary<int, ulong>>();
                foreach (DegreeType degree in Constants.RealDegrees)
                {
                    DegreeHash[degree] = new Dictionary<int, ulong>();
                }
                CampusHash = new Dictionary<CampusType, Dictionary<Vertex, ulong>>();
                CampusHash[CampusType.Traditional] = new Dictionary<Vertex, ulong>();
                CampusHash[CampusType.Super] = new Dictionary<Vertex, ulong>();
                LinkHash = new Dictionary<Edge, ulong>();

                StartUpHash = new Dictionary<bool, Dictionary<int, ulong>>();
                StartUpHash[true] = new Dictionary<int, ulong>();
                StartUpHash[false] = new Dictionary<int, ulong>();

                MostInfoHash = new Dictionary<MostInfoType, ulong>();
            }
        }

        #endregion
    }
}