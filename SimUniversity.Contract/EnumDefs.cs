namespace MingStar.SimUniversity.Contract
{
    public enum GamePhase
    {
        Setup1,
        Setup2,
        Play
    }

    public enum CampusType
    {
        Traditional,
        Super
    }

    public enum Color
    {
        Red,
        Blue,
        White,
        Orange,
        Green,
        Brown
    }

    public enum DegreeType
    {
        None = 0,
        Wood, // BEng
        Brick, // BSc
        Grain, // LLB or law
        Ore, //MBA
        Sheep // Art
    }

    /// <summary>
    /// From top, anti-clockwise
    /// </summary>
    public enum EdgeOrientation
    {
        TopLeft = 0,
        BottomLeft,
        Bottom,
        BottomRight,
        TopRight,
        Top
    }

    /// <summary>
    /// From top-left, anti-clockwise
    /// </summary>
    public enum VertexOrientation
    {
        TopLeft = 0,
        Left,
        BottomLeft,
        BottomRight,
        Right,
        TopRight
    }

    public enum MostInfoType
    {
        FailedStartUps,
        Links,
    }
}