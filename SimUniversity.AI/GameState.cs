using System;
using System.Collections.Generic;
using System.Linq;
using MingStar.SimUniversity.Contract;
using MingStar.Utilities.Linq;

namespace MingStar.SimUniversity.AI
{
    public class GameState
    {
        public GameState(int numberOfScores, double initScore)
        {
            Scores = new double[numberOfScores];
            Scores.Fill(initScore);
        }

        public GameState(double[] scores)
        {
            Scores = scores;
        }

        public GameState(IPlayerMove move, double[] scores)
        {
            BestMove = move;
            Scores = scores;
        }

        public IPlayerMove BestMove { get; private set; }
        public GameState NextGameState { get; internal set; }
        public double[] Scores { get; private set; }

        public double this[int index]
        {
            get { return Scores[index]; }
            set { Scores[index] = value; }
        }

        internal void TakeIfBetter(GameState nextScoredMove, int scoreIndex, IPlayerMove thisMove)
        {
            double otherOffsetScore = nextScoredMove[scoreIndex] - Scores[scoreIndex];
            bool takeIt = false;
            if (Math.Abs(otherOffsetScore) < double.Epsilon)
            {
                if (nextScoredMove.Scores.Sum() < Scores.Sum())
                {
                    takeIt = true;
                }
            }
            else if (otherOffsetScore < 0.0)
            {
                return; // not take it
            }
            else if (otherOffsetScore > 0.0)
            {
                takeIt = true;
            }
            if (takeIt)
            {
                NextGameState = nextScoredMove;
                Scores = nextScoredMove.Scores;
                BestMove = thisMove;
            }
        }

        public List<IPlayerMove> GetMoveList()
        {
            var list = new List<IPlayerMove>();
            AddToList(list);
            return list;
        }

        private void AddToList(List<IPlayerMove> list)
        {
            if (BestMove != null)
            {
                list.Add(BestMove);
                if (NextGameState != null)
                {
                    NextGameState.AddToList(list);
                }
            }
        }
    }
}