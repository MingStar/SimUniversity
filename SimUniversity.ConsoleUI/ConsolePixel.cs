using System;
using System.Collections.Generic;
using MingStar.SimUniversity.Board;
using MingStar.SimUniversity.Contract;

namespace MingStar.SimUniversity.ConsoleUI
{
    public class ConsolePixel
    {
        public static ConsoleColor DefaultForeColor = ConsoleColor.White;
        public static ConsoleColor DefaultBackColor = ConsoleColor.Black;

        private static readonly ConsolePixel EmptyVertex = new ConsolePixel
                                                               {
                                                                   Char = '.'
                                                               };

        public static readonly ConsolePixel EmptyPixel = new ConsolePixel();

        private static readonly Dictionary<ConsoleColor, ConsoleColor> _matchColors =
            new Dictionary<ConsoleColor, ConsoleColor>();

        public ConsoleColor BackColor = ConsoleColor.Black;
        public char Char = ' ';
        public ConsoleColor ForeColor = ConsoleColor.White;

        static ConsolePixel()
        {
            _matchColors[ConsoleColor.Yellow] = ConsoleColor.Black;
            _matchColors[ConsoleColor.White] = ConsoleColor.Black;
        }

        public ConsolePixel()
        {
        }

        public ConsolePixel(char chr)
        {
            Char = chr;
        }

        public static ConsolePixel GetVertexPixel(Vertex vertex)
        {
            if (vertex.Campus == null)
            {
                if (!vertex.IsFreeToBuildCampus())
                {
                    return EmptyVertex;
                }
                else
                {
                    ITradingSite site = vertex.TradingSite;
                    if (site == null)
                    {
                        return new ConsolePixel
                                   {
                                       Char = '*'
                                   };
                    }
                    else if (site == TradingSite.Instance)
                    {
                        return new ConsolePixel
                                   {
                                       Char = '?'
                                   };
                    }
                    else
                    {
                        return new ConsolePixel
                                   {
                                       Char = Transform(((SpecialTradingSite) site).TradeOutDegree)
                                   };
                    }
                }
            }
            else
            {
                return new ConsolePixel
                           {
                               BackColor = Transform(vertex.Campus.Color, true),
                               ForeColor = ConsoleColor.Black,
                               Char = (vertex.Campus.Type == CampusType.Traditional) ? 'T' : 'S',
                           };
            }
        }

        public static ConsolePixel GetEdgePixel(Edge edge, char value)
        {
            ConsoleColor backColor = DefaultBackColor;
            ConsoleColor foreColor = DefaultForeColor;
            if (edge.Color.HasValue)
            {
                backColor = Transform(edge.Color, true);
                foreColor = ConsoleColor.Black;
            }
            return new ConsolePixel
                       {
                           Char = value,
                           BackColor = backColor,
                           ForeColor = foreColor
                       };
        }

        public static char Transform(DegreeType degree)
        {
            switch (degree)
            {
                case DegreeType.Brick:
                    return 'b';
                case DegreeType.Grain:
                    return 'g';
                case DegreeType.Ore:
                    return 'o';
                case DegreeType.Sheep:
                    return 's';
                case DegreeType.Wood:
                    return 'w';
                default:
                    throw new Exception("Unknown degree type: " + degree);
            }
        }

        public static ConsoleColor Transform(Color? color, bool isBack)
        {
            if (color != null)
            {
                switch (color.Value)
                {
                    case Color.Red:
                        return ConsoleColor.Red;
                    case Color.Blue:
                        return ConsoleColor.Cyan;
                    case Color.Orange:
                        return ConsoleColor.Yellow;
                    case Color.White:
                        return ConsoleColor.White;
                }
            }
            return isBack ? ConsoleColor.Black : ConsoleColor.White;
        }
    }
}