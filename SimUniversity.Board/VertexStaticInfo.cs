using System.Collections.Generic;
using MingStar.SimUniversity.Contract;

namespace MingStar.SimUniversity.Board
{
    public class VertexStaticInfo
    {
        private static readonly Dictionary<VertexOrientation, VertexStaticInfo> s_lookUp;

        static VertexStaticInfo()
        {
            s_lookUp = new Dictionary<VertexOrientation, VertexStaticInfo>();
            s_lookUp[VertexOrientation.TopLeft] = 
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
            s_lookUp[VertexOrientation.Left] = 
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
            s_lookUp[VertexOrientation.BottomLeft] = 
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
            s_lookUp[VertexOrientation.BottomRight] = new VertexStaticInfo
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
            s_lookUp[VertexOrientation.Right] = new VertexStaticInfo
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
            s_lookUp[VertexOrientation.TopRight] = new VertexStaticInfo
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
        public RelativePosition[] RelativePositions { get; private set; }

        public static VertexStaticInfo Get(VertexOrientation vo)
        {
            return s_lookUp[vo];
        }

        #region Nested type: RelativePosition

        public class RelativePosition
        {
            internal Offset Offset;
            internal VertexOrientation Orientation;
        }

        #endregion
    }
}