namespace MingStar.SimUniversity.Contract
{
    public class HexagonOffset : IntTuple
    {
        public HexagonOffset(int x, int y)
            : base(x, y)
        {
        }
    }

    public class HexagonOffsetWith<T> : HexagonOffset
    {
        public T Item { get; private set; }

        public HexagonOffsetWith(int x, int y, T item)
            : base(x, y)
        {
            Item = item;
        }
    }
}