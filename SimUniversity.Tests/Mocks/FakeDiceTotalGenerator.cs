using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MingStar.SimUniversity.Game;

namespace MingStar.SimUniversity.Tests.Mocks
{
    public class FakeDiceTotalGenerator : IDiceTotalRoll
    {
        private int? _nextRoll = null;

        public void SetNextRoll(int number)
        {
            _nextRoll = number;
        }

        public int GetNextDiceTotal()
        {
            if (_nextRoll.HasValue)
            {
                int tmp = _nextRoll.Value;
                _nextRoll = null;
                return tmp;
            }
            throw new Exception(string.Format("{0}: Dice roll is not defined!", typeof(FakeDiceTotalGenerator).Name));
        }
    }
}
