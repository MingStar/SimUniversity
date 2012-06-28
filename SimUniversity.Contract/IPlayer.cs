using System.Collections.Generic;

namespace MingStar.SimUniversity.Contract
{
    public interface IPlayer
    {
        string Name { get; }
        List<IPlayerMove> MakeMoves(IGame game);
    }
}