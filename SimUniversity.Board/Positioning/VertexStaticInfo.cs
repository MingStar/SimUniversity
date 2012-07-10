using System.Collections.Generic;
using MingStar.SimUniversity.Contract;

namespace MingStar.SimUniversity.Board.Positioning
{
    public class VertexStaticInfo
    {
        private static readonly Dictionary<VertexOrientation, VertexStaticInfo> _lookUp;

        static VertexStaticInfo()
        {
            _lookUp = new Dictionary<VertexOrientation, VertexStaticInfo>();
            _lookUp[VertexOrientation.TopLeft] =
                new VertexStaticInfo
                    {
                        AdjacentVertexOrientations =
                            new[] {VertexOrientation.TopRight, VertexOrientation.Left},
                        AdjacentEdgeOrientations =
                            new[] {EdgeOrientation.Top, EdgeOrientation.TopLeft},
                        RelativePositions = new[]
                                                {
                                                    new RelativePosition
                                                        {
                                                            Offset =
                                                                EdgeStaticInfo.Get(
                                                                    EdgeOrientation.
                                                                        TopLeft).
                                                                HexagonOffset,
                                                            Orientation =
                                                                VertexOrientation.
                                                                Right
                                                        },
                                                    new RelativePosition
                                                        {
                                                            Offset =
                                                                EdgeStaticInfo.Get(
                                                                    EdgeOrientation.
                                                                        Top).
                                                                HexagonOffset,
                                                            Orientation =
                                                                VertexOrientation.
                                                                BottomLeft
                                                        }
                                                },
                    };
            _lookUp[VertexOrientation.Left] =
                new VertexStaticInfo
                    {
                        AdjacentVertexOrientations =
                            new[]
                                {VertexOrientation.TopLeft, VertexOrientation.BottomLeft},
                        AdjacentEdgeOrientations =
                            new[] {EdgeOrientation.TopLeft, EdgeOrientation.BottomLeft},
                        RelativePositions = new[]
                                                {
                                                    new RelativePosition
                                                        {
                                                            Offset =
                                                                EdgeStaticInfo.Get(
                                                                    EdgeOrientation.
                                                                        TopLeft).
                                                                HexagonOffset,
                                                            Orientation =
                                                                VertexOrientation.
                                                                BottomRight
                                                        },
                                                    new RelativePosition
                                                        {
                                                            Offset =
                                                                EdgeStaticInfo.Get(
                                                                    EdgeOrientation.
                                                                        BottomLeft).
                                                                HexagonOffset,
                                                            Orientation =
                                                                VertexOrientation.
                                                                TopRight
                                                        }
                                                }
                    };
            _lookUp[VertexOrientation.BottomLeft] =
                new VertexStaticInfo
                    {
                        AdjacentVertexOrientations =
                            new[]
                                {
                                    VertexOrientation.Left,
                                    VertexOrientation.BottomRight
                                },
                        AdjacentEdgeOrientations =
                            new[]
                                {
                                    EdgeOrientation.BottomLeft, EdgeOrientation.Bottom
                                },
                        RelativePositions = new[]
                                                {
                                                    new RelativePosition
                                                        {
                                                            Offset =
                                                                EdgeStaticInfo.Get(
                                                                    EdgeOrientation
                                                                        .BottomLeft)
                                                                .HexagonOffset,
                                                            Orientation =
                                                                VertexOrientation.
                                                                Right,
                                                        },
                                                    new RelativePosition
                                                        {
                                                            Offset =
                                                                EdgeStaticInfo.Get(
                                                                    EdgeOrientation
                                                                        .Bottom).
                                                                HexagonOffset,
                                                            Orientation =
                                                                VertexOrientation.
                                                                TopLeft
                                                        }
                                                }
                    };
            _lookUp[VertexOrientation.BottomRight] = new VertexStaticInfo
                                                          {
                                                              AdjacentVertexOrientations =
                                                                  new[]
                                                                      {
                                                                          VertexOrientation.BottomLeft,
                                                                          VertexOrientation.Right
                                                                      },
                                                              AdjacentEdgeOrientations =
                                                                  new[]
                                                                      {
                                                                          EdgeOrientation.Bottom,
                                                                          EdgeOrientation.BottomRight
                                                                      },
                                                              RelativePositions = new[]
                                                                                      {
                                                                                          new RelativePosition
                                                                                              {
                                                                                                  Offset =
                                                                                                      EdgeStaticInfo.Get
                                                                                                      (EdgeOrientation.
                                                                                                           Bottom).
                                                                                                      HexagonOffset,
                                                                                                  Orientation =
                                                                                                      VertexOrientation.
                                                                                                      TopRight,
                                                                                              },
                                                                                          new RelativePosition
                                                                                              {
                                                                                                  Offset =
                                                                                                      EdgeStaticInfo.Get
                                                                                                      (EdgeOrientation.
                                                                                                           BottomRight).
                                                                                                      HexagonOffset,
                                                                                                  Orientation =
                                                                                                      VertexOrientation.
                                                                                                      Left
                                                                                              }
                                                                                      }
                                                          };
            _lookUp[VertexOrientation.Right] = new VertexStaticInfo
                                                    {
                                                        AdjacentVertexOrientations =
                                                            new[]
                                                                {
                                                                    VertexOrientation.BottomRight,
                                                                    VertexOrientation.TopRight
                                                                },
                                                        AdjacentEdgeOrientations =
                                                            new[]
                                                                {EdgeOrientation.BottomRight, EdgeOrientation.TopRight},
                                                        RelativePositions = new[]
                                                                                {
                                                                                    new RelativePosition
                                                                                        {
                                                                                            Offset =
                                                                                                EdgeStaticInfo.Get(
                                                                                                    EdgeOrientation.
                                                                                                        BottomRight).
                                                                                                HexagonOffset,
                                                                                            Orientation =
                                                                                                VertexOrientation.
                                                                                                TopLeft
                                                                                        },
                                                                                    new RelativePosition
                                                                                        {
                                                                                            Offset =
                                                                                                EdgeStaticInfo.Get(
                                                                                                    EdgeOrientation.
                                                                                                        TopRight).
                                                                                                HexagonOffset,
                                                                                            Orientation =
                                                                                                VertexOrientation.
                                                                                                BottomLeft
                                                                                        }
                                                                                }
                                                    };
            _lookUp[VertexOrientation.TopRight] = new VertexStaticInfo
                                                       {
                                                           AdjacentVertexOrientations =
                                                               new[]
                                                                   {VertexOrientation.Right, VertexOrientation.TopLeft},
                                                           AdjacentEdgeOrientations =
                                                               new[] {EdgeOrientation.TopRight, EdgeOrientation.Top},
                                                           RelativePositions = new[]
                                                                                   {
                                                                                       new RelativePosition
                                                                                           {
                                                                                               Offset =
                                                                                                   EdgeStaticInfo.Get(
                                                                                                       EdgeOrientation.
                                                                                                           TopRight).
                                                                                                   HexagonOffset,
                                                                                               Orientation =
                                                                                                   VertexOrientation.
                                                                                                   Left,
                                                                                           },
                                                                                       new RelativePosition
                                                                                           {
                                                                                               Offset =
                                                                                                   EdgeStaticInfo.Get(
                                                                                                       EdgeOrientation.
                                                                                                           Top).
                                                                                                   HexagonOffset,
                                                                                               Orientation =
                                                                                                   VertexOrientation.
                                                                                                   BottomRight
                                                                                           }
                                                                                   }
                                                       };
        }

        public EdgeOrientation[] AdjacentEdgeOrientations { get; private set; }
        public VertexOrientation[] AdjacentVertexOrientations { get; private set; }
        internal RelativePosition[] RelativePositions { get; private set; }

        public static VertexStaticInfo Get(VertexOrientation vo)
        {
            return _lookUp[vo];
        }

        #region Nested type: RelativePosition

        internal struct RelativePosition
        {
            public HexagonOffset Offset;
            public VertexOrientation Orientation;
        }

        #endregion
    }
}