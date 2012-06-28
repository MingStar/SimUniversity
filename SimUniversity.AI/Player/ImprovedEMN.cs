using System.Collections.ObjectModel;
using MingStar.SimUniversity.AI.Evaluation;
using MingStar.SimUniversity.Contract;
using MingStar.SimUniversity.Game;

namespace MingStar.SimUniversity.AI.Player
{
    public class ImprovedEMN : ExpectiMaxN
    {
        private static ImprovedEMN _instance;

        private readonly TranspositionTable _transTable;
        private Game.Game _game;

        public ImprovedEMN(Game.Game game, GameScores scores) : base(scores)
        {
            _game = game;
            //_transTable = new TranspositionTable();
        }

        private ImprovedEMN()
        {
        }

        public static ImprovedEMN Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ImprovedEMN();
                }
                return _instance;
            }
        }


        public override GameState SearchBestMoves(Game.Game game)
        {
            return SearchBestMoves(game, 4, game.CurrentUniversityIndex);
        }

        private GameState SearchBestMoves(Game.Game game, int depth, int playerIndex)
        {
            if ((game.CurrentUniversityIndex == playerIndex && game.HasWinner())
                || depth == 0)
            {
                return new GameState(Evaluate(game));
            }
            ReadOnlyCollection<IPlayerMove> allMoves = game.GenerateAllMoves();
            if (allMoves.Count == 1 && playerIndex == game.CurrentUniversityIndex)
            {
                return new GameState(allMoves[0], Evaluate(game));
            }
            var bestMoves = new GameState(game.NumberOfUniversities, double.MinValue);
            foreach (IPlayerMove move in allMoves)
            {
                if (move is IProbabilityPlayerMove)
                {
                    var expectedScores = new GameState(game.NumberOfUniversities, 0);
                    double totalProbability = 0.0;
                    foreach (IProbabilityPlayerMove possibleMove in ((IProbabilityPlayerMove) move).AllProbabilityMoves)
                    {
                        double thisProbability = possibleMove.Probability.Value;
                        if (thisProbability < 0.06)
                        {
                            continue; // skip 2, 12, 3, 11 for end turn
                        }
                        totalProbability += thisProbability;
                        GameState nextScores = GetNextScores(game, depth, playerIndex, possibleMove);
                        for (int i = 0; i < expectedScores.Scores.Length; ++i)
                        {
                            expectedScores[i] += thisProbability*nextScores[i];
                        }
                    }
                    for (int i = 0; i < game.NumberOfUniversities; ++i)
                    {
                        expectedScores[i] /= totalProbability;
                    }
                    // trust it with probability
                    if (RandomGenerator.Random.NextDouble() < totalProbability)
                    {
                        bestMoves.TakeIfBetter(expectedScores, game.CurrentUniversityIndex, move);
                    }
                }
                else //move is BuildCampusMove || move is BuildLinkMove || move is TradingMove)
                {
                    GameState nextScores = GetNextScores(game, depth, playerIndex, move);
                    bestMoves.TakeIfBetter(nextScores, game.CurrentUniversityIndex, move);
                }
            }
            return bestMoves;
        }

        private GameState GetNextScores(Game.Game game, int depth, int playerIndex, IPlayerMove move)
        {
            depth -= 1;
            game.ApplyMove(move);
            GameState nextBestScoredMoves = null;
            if (_transTable != null)
            {
                nextBestScoredMoves = _transTable.GetBestScoredMoves(game.Hash, depth);
            }
            if (nextBestScoredMoves == null)
            {
                nextBestScoredMoves = SearchBestMoves(game, depth, playerIndex);
                if (_transTable != null)
                {
                    _transTable.Remember(game.Hash, nextBestScoredMoves, depth);
                }
            }
            game.UndoMove();
            return nextBestScoredMoves;
        }
    }
}