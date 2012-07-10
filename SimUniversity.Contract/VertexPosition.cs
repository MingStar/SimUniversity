﻿namespace MingStar.SimUniversity.Contract
{
    public class VertexPosition
    {
        public VertexPosition(int x, int y, VertexOrientation vo)
            : this(new Position(x, y), vo)
        {
        }

        public VertexPosition(Position position, VertexOrientation vo)
        {
            HexPosition = position;
            Orientation = vo;
        }

        public Position HexPosition { get; private set; }
        public VertexOrientation Orientation { get; private set; }

        public override string ToString()
        {
            return string.Format("{0} - {1} vertex", HexPosition, Orientation);
        }
    }
}