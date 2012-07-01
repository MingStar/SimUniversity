namespace MingStar.SimUniversity.Contract
{
    public class Position : IntTuple
    {
        public Position(int x, int y)
            : base(x, y)
        {
        }

        public override bool Equals(object obj)
        {
            if (obj is Position)
            {
                var p = (Position) obj;
                return (X == p.X && Y == p.Y);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return X.GetHashCode() + Y.GetHashCode();
        }

        public Position Add(HexagonOffset offset)
        {
            return new Position(X + offset.X, Y + offset.Y);
        }

        public Position Add(int x, int y)
        {
            return new Position(X + x, Y + y);
        }
    }
}