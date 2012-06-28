namespace MingStar.SimUniversity.Contract
{
    public class EdgePosition
    {
        public EdgePosition(int x, int y, EdgeOrientation eo)
            : this(new Position(x, y), eo)
        {
        }

        public EdgePosition(Position position, EdgeOrientation eo)
        {
            HexPosition = position;
            Orientation = eo;
        }

        public Position HexPosition { get; private set; }
        public EdgeOrientation Orientation { get; private set; }

        public override string ToString()
        {
            return string.Format("{0} - {1} edge", HexPosition, Orientation);
        }
    }
}