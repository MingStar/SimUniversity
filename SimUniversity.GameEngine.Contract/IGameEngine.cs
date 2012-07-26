using System.Collections.Generic;

namespace SimUniversity.GameEngine.Contract
{
    public interface IGameEngine
    {
        string Login(string username, string password);        
        List<GameSummaryDto> ListOpenGames(string sessionToken);
        int CreateGame(string sessionToken);
        void JoinGame(string sessionToken, int gameId);
        void LeaveGame(string sessionToken, int gameId);
        GameDto GetGameById(string sessionToken, int gameId);
        void MakeEndTurnMove(string sessionToken, int gameId);
    }
}