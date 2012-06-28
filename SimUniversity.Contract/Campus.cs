namespace MingStar.SimUniversity.Contract
{
    public class Campus
    {
        public Campus(CampusType type, Color color)
        {
            Type = type;
            Color = color;
        }

        public CampusType Type { get; private set; }
        public Color Color { get; private set; }

        public override string ToString()
        {
            return string.Format("{0} {1} campus", Color, Type);
        }
    }
}