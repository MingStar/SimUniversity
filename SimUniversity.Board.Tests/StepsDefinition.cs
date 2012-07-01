using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MingStar.SimUniversity.Board.Constructor;
using NUnit.Framework;
using TechTalk.SpecFlow;
using MingStar.SimUniversity.Board;

namespace SimUniversity.Board.Tests
{
    [Binding]
    public class StepsDefinition
    {
        private MingStar.SimUniversity.Board.Board _board;

        [When(@"I set up the beginner board for Catan")]
        public void WhenISetUpTheBeginnerBoardForCatan()
        {
            _board = (new SettlerBeginnerBoardConstructor()).Board;
        }

        [Then(@"there should be (.*) hexagons on the board")]
        public void ThenThereShouldBeHexagonsOnTheBoard(int expected)
        {
            Assert.AreEqual(expected, _board.GetHexagons().Count());
        }

        [Then(@"there should be (.*) vectices on the board")]
        public void ThenThereShouldBeVecticesOnTheBoard(int expected)
        {
            Assert.AreEqual(expected, _board.GetVertices().Count());
        }

        [Then(@"there should be (.*) edges on the board")]
        public void ThenThereShouldBeEdgesOnTheBoard(int expected)
        {
            Assert.AreEqual(expected, _board.GetEdges().Count());
        }

        [Then(@"the details of hexagons should be the following:")]
        public void ThenTheDetailsOfHexagonsShouldBeTheFollowing(Table table)
        {
            foreach (var row in table.Rows)
            {
                var hex = _board[int.Parse(row["X"]), int.Parse(row["Y"])];
                Assert.IsNotNull(hex);
                Assert.AreEqual(int.Parse(row["Number Marker"]), hex.ID);
                Assert.AreEqual(row["Student"], hex.Degree.ToString());
                var adj = hex.Adjacent;
                Assert.AreEqual(int.Parse(row["Adj. # of hexes"]), adj.Hexagons.Count);
                Assert.AreEqual(int.Parse(row["Adj. # of vertices"]), adj.Vertices.Count);
                Assert.AreEqual(int.Parse(row["Adj. # of edges"]), adj.Edges.Count);                
            }
        }

    }
}
