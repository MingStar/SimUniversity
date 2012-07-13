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
        public IEnumerable<int> _cards;
        public IEnumerator<int> _cardsEnumerator;

        public int GetNextDiceTotal()
        {
            if (_cards == null || !_cardsEnumerator.MoveNext())
            {
                _cards = GenerateCards();
                _cardsEnumerator = _cards.GetEnumerator();
                _cardsEnumerator.MoveNext();
            }
            return _cardsEnumerator.Current;
        }

        private IEnumerable<int> GenerateCards()
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

        public bool IsNextStartUpSuccessful()
        {
            return RandomGenerator.Next(5) == 0;
        }
    }
}
