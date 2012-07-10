using System.Collections.Generic;
using System.Linq;
using MingStar.SimUniversity.AI.Evaluation;
using MingStar.SimUniversity.Contract;
using MingStar.SimUniversity.Game.Move;
using MingStar.Utilities.Linq;

namespace MingStar.SimUniversity.AI.Player
{
    public class Smarter : IPlayer
    {
        private readonly SetupPlayer _setupPlayer = new SetupPlayer(new GameEvaluation(new GameScores()));

        #region IPlayer Members

        public List<IPlayerMove> MakeMoves(IGame game)
        {
            if (game.CurrentPhase != GamePhase.Play)
            {
                return _setupPlayer.MakeMoves(game);
            }
            IEnumerable<ScoredMove> result = (from move in game.GenerateAllMoves().Shuffle()
                                              select ScoreMove(game, move));
            IOrderedEnumerable<ScoredMove> result2 = (from scoredMove in result
                                                      orderby scoredMove.Score descending
                                                      select scoredMove);
            return new List<IPlayerMove> {result2.First().Move};
        }

        public string Name
        {
            get { return "Smarter"; }
        }

        #endregion

        private ScoredMove ScoreMove(IGame game, IPlayerMove move)
        {
            double score = 0;
            if (move is BuildCampusMove)
            {
                var bMove = (BuildCampusMove) move;
                score = bMove.CampusType == CampusType.Super ? 30 : 20;
            }
            else if (move is BuildLinkMove)
            {
                score = 15;
                var bMove = (BuildLinkMove) move;
                if (game.IBoard[bMove.WhereAt].Adjacent.Vertices.Any(vertex => vertex.IsFreeToBuildCampus()))
                {
                    score += 1;
                }
            }
            else if (move is TradingMove)
            {
                score = 10;
            }
            else if (move is TryStartUpMove)
            {
                if (game.CurrentIUniversity.Students.Values.Sum() > 5)
                {
                    score = 5;
                }
                else
                {
                    score = -5;
                }
            }
            return new ScoredMove(move, score);
        }
    }
}