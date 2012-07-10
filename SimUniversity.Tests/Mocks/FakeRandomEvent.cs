using System;
using MingStar.SimUniversity.Game.Random;

namespace MingStar.SimUniversity.Tests.Mocks
{
    public class FakeRandomEvent : IRandomEvent
    {
        private bool? _isNextStartUpSuccessful;
        private int? _nextRoll;

        #region IRandomEvent Members

        public int GetNextDiceTotal()
        {
            if (_nextRoll.HasValue)
            {
                int tmp = _nextRoll.Value;
                _nextRoll = null;
                return tmp;
            }
            throw Exception("Dice roll is not defined!");
        }

        public bool IsNextStartUpSuccessful()
        {
            if (_isNextStartUpSuccessful.HasValue)
            {
                bool tmp = _isNextStartUpSuccessful.Value;
                _isNextStartUpSuccessful = null;
                return tmp;
            }
            throw Exception("Start up success chance is not defined!");
        }

        #endregion

        public void SetNextRoll(int number)
        {
            _nextRoll = number;
        }

        public void SetNextStartUpSuccessful(bool yes)
        {
            _isNextStartUpSuccessful = yes;
        }

        private static Exception Exception(string message)
        {
            return new Exception(string.Format("{0}: {1}", typeof (FakeRandomEvent).Name, message));
        }
    }
}