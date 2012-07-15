using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MingStar.SimUniversity.Contract;
using MingStar.Utilities.Linq;

namespace MingStar.SimUniversity.Game.Random
{
    /// <summary>
    /// Uses 36 dice cards to simulate the dice rolls, to ensure the distribution is always even.
    /// </summary>
    public class DiceCardRandomEvent : IRandomEvent
    {
        private IEnumerable<int> _diceCards;
        private IEnumerator<int> _diceCardsEnumerator;

        public int GetNextDiceTotal()
        {
            if (_diceCards == null || !_diceCardsEnumerator.MoveNext())
            {
                _diceCards = GenerateDiceCards();
                _diceCardsEnumerator = _diceCards.GetEnumerator();
                _diceCardsEnumerator.MoveNext();
            }
            return _diceCardsEnumerator.Current;
        }

        private IEnumerable<int> GenerateDiceCards()
        {
            var cards = new List<int>();
            foreach (var item in GameConstants.DiceRollNumber2Chance)
            {
                for (var i = 0; i < item.Value; ++i)
                {
                    cards.Add(item.Key);
                }
            }
            return cards.Shuffle();
        }

        private IEnumerable<int> _startupCards;
        private IEnumerator<int> _startupCardsEnumerator;
        private readonly int[] _startupSuccessChance = new [] { 0, 0, 0, 0, 1};

        public bool IsNextStartUpSuccessful()
        {
            if (_startupCards == null || !_startupCardsEnumerator.MoveNext())
            {
                _startupCards = _startupSuccessChance.Shuffle();
                _startupCardsEnumerator = _startupCards.GetEnumerator();
                _startupCardsEnumerator.MoveNext();
            }
            return _startupCardsEnumerator.Current == 1;
        }
    }
}
