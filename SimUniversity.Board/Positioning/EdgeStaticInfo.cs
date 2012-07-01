using System.Collections.Generic;
using MingStar.SimUniversity.Contract;

namespace MingStar.SimUniversity.Board
{
    public class EdgeStaticInfo
    {
        private static readonly Dictionary<EdgeOrientation, EdgeStaticInfo> _staticInfoDict;

        static EdgeStaticInfo()
        {
            _staticInfoDict = new Dictionary<EdgeOrientation, EdgeStaticInfo>();
            _staticInfoDict[EdgeOrientation.TopLeft] =
                new EdgeStaticInfo
                    {
                        AdjacentEdgeOrientations = new[] {EdgeOrientation.Top, EdgeOrientation.BottomLeft},
                        AdjacentVertexOrientations = new[] {VertexOrientation.TopLeft, VertexOrientation.Left},
                        OppositeEdge = EdgeOrientation.BottomRight,
                        HexagonOffset = new HexagonOffset(-1, 1),
                        AdjacentEdgeOffsets = new[]
                                                  {
                                                      new EdgePosition(0, 1, EdgeOrientation.BottomLeft),
                                                      new EdgePosition(-1, 1, EdgeOrientation.TopRight),
                                                      new EdgePosition(-1, 1, EdgeOrientation.Bottom),
                                                      new EdgePosition(-1, 0, EdgeOrientation.Top),
                                                  },
                    };
            _staticInfoDict[EdgeOrientation.BottomLeft] =
                new EdgeStaticInfo
                    {
                        AdjacentEdgeOrientations = new[] {EdgeOrientation.TopLeft, EdgeOrientation.Bottom},
                        AdjacentVertexOrientations = new[] {VertexOrientation.Left, VertexOrientation.BottomLeft},
                        OppositeEdge = EdgeOrientation.TopRight,
                        HexagonOffset = new HexagonOffset(-1, 0),
                        AdjacentEdgeOffsets = new[]
                                                  {
                                                      new EdgePosition(-1, 1, EdgeOrientation.Bottom),
                                                      new EdgePosition(-1, 0, EdgeOrientation.Top),
                                                      new EdgePosition(-1, 0, EdgeOrientation.BottomRight),
                                                      new EdgePosition(0, -1, EdgeOrientation.TopLeft),
                                                  },
                    };
            _staticInfoDict[EdgeOrientation.Bottom] =
                new EdgeStaticInfo
                    {
                        AdjacentEdgeOrientations = new[] {EdgeOrientation.BottomLeft, EdgeOrientation.BottomRight},
                        AdjacentVertexOrientations = new[] {VertexOrientation.BottomLeft, VertexOrientation.BottomRight},
                        OppositeEdge = EdgeOrientation.Top,
                        HexagonOffset = new HexagonOffset(0, -1),
                        AdjacentEdgeOffsets = new[]
                                                  {
                                                      new EdgePosition(-1, 0, EdgeOrientation.BottomRight),
                                                      new EdgePosition(0, -1, EdgeOrientation.TopLeft),
                                                      new EdgePosition(0, -1, EdgeOrientation.TopRight),
                                                      new EdgePosition(1, -1, EdgeOrientation.BottomLeft),
                                                  },
                    };
            _staticInfoDict[EdgeOrientation.BottomRight] =
                new EdgeStaticInfo
                    {
                        AdjacentEdgeOrientations = new[] {EdgeOrientation.Bottom, EdgeOrientation.TopRight},
                        AdjacentVertexOrientations = new[] {VertexOrientation.BottomRight, VertexOrientation.Right},
                        OppositeEdge = EdgeOrientation.TopLeft,
                        HexagonOffset = new HexagonOffset(1, -1),
                        AdjacentEdgeOffsets = new[]
                                                  {
                                                      new EdgePosition(0, -1, EdgeOrientation.TopRight),
                                                      new EdgePosition(1, -1, EdgeOrientation.BottomLeft),
                                                      new EdgePosition(1, -1, EdgeOrientation.Top),
                                                      new EdgePosition(1, 0, EdgeOrientation.Bottom),
                                                  },
                    };
            _staticInfoDict[EdgeOrientation.TopRight] =
                new EdgeStaticInfo
                    {
                        AdjacentEdgeOrientations = new[] {EdgeOrientation.BottomRight, EdgeOrientation.Top},
                        AdjacentVertexOrientations = new[] {VertexOrientation.Right, VertexOrientation.TopRight},
                        OppositeEdge = EdgeOrientation.BottomLeft,
                        HexagonOffset = new HexagonOffset(1, 0),
                        AdjacentEdgeOffsets = new[]
                                                  {
                                                      new EdgePosition(1, -1, EdgeOrientation.Top),
                                                      new EdgePosition(1, 0, EdgeOrientation.Bottom),
                                                      new EdgePosition(1, 0, EdgeOrientation.TopLeft),
                                                      new EdgePosition(0, 1, EdgeOrientation.BottomRight),
                                                  },
                    };
            _staticInfoDict[EdgeOrientation.Top] =
                new EdgeStaticInfo
                    {
                        AdjacentEdgeOrientations = new[] {EdgeOrientation.TopRight, EdgeOrientation.TopLeft},
                        AdjacentVertexOrientations = new[] {VertexOrientation.TopRight, VertexOrientation.TopLeft},
                        OppositeEdge = EdgeOrientation.Bottom,
                        HexagonOffset = new HexagonOffset(0, 1),
                        AdjacentEdgeOffsets = new[]
                                                  {
                                                      new EdgePosition(1, 0, EdgeOrientation.TopLeft),
                                                      new EdgePosition(0, 1, EdgeOrientation.BottomRight),
                                                      new EdgePosition(0, 1, EdgeOrientation.BottomLeft),
                                                      new EdgePosition(-1, 1, EdgeOrientation.TopRight),
                                                  },
                    };
        }

        public EdgeOrientation[] AdjacentEdgeOrientations { get; private set; }
        public VertexOrientation[] AdjacentVertexOrientations { get; private set; }
        public EdgeOrientation OppositeEdge { get; private set; }
        public HexagonOffset HexagonOffset { get; private set; }
        public EdgePosition[] AdjacentEdgeOffsets { get; private set; }

        internal static EdgeStaticInfo Get(EdgeOrientation eo)
        {
            return _staticInfoDict[eo];
        }
    }
}