namespace MingStar.SimUniversity.Board
{
    /// <summary>
    /// base class for hexagons, edges and vertices
    /// </summary>
    public abstract class Place
    {
        protected Place()
        {
            Adjacent = new AdjacentInfo();
        }

        public AdjacentInfo Adjacent { get; set; }

        internal abstract void Reset();
    }
}