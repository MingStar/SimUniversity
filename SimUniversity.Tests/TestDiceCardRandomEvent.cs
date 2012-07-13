using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MingStar.SimUniversity.Contract;
using MingStar.SimUniversity.Game.Random;
using NUnit.Framework;

namespace MingStar.SimUniversity.Tests
{
    public class TestDiceCardRandomEvent
    {
        [Test]
        public void TestDiceCardReturnCorrectNumbers()
        {
            // setup
            var result = new Dictionary<int, int>();
            var cardGen = new DiceCardRandomEvent();
            var testRun = 10;
            // run
            for (var i = 0; i < 36 * testRun; i++)
            {
                var roll = cardGen.GetNextDiceTotal();
                if (!result.ContainsKey(roll))
                {
                    result[roll] = 0;
                }
                result[roll]++;
            }
            // assert
            foreach (var roll in result.Keys)
            {
                Assert.AreEqual(GameConstants.DiceRollNumber2Chance[roll]*testRun, result[roll]);
            }
        }
    }
}
