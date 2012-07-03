using System;
using System.Collections.Generic;
using System.Linq;
using MingStar.SimUniversity.Board.Constructor;
using MingStar.SimUniversity.Contract;
using MingStar.SimUniversity.Game.Games;
using NUnit.Framework;
using TechTalk.SpecFlow;
using MingStar.SimUniversity.Board;

namespace MingStar.SimUniversity.Tests
{
    [Binding]
    public class StepsDefinition
    {
        private Board.Board _board;
        private Game.Game _game;

        [When(@"I set up the beginner board for Catan")]
        public void WhenISetUpTheBeginnerBoardForCatan()
        {
            _board = (new SettlerBeginnerBoardConstructor()).Board;
        }

        [When(@"I set up the basic board for Catan")]
        public void WhenISetUpTheBasicBoardForCatan()
        {
            _board = (new SettlerBoardConstructor()).Board;
        }

        [Then(@"there should be (.*) hexagons on the board")]
        public void ThenThereShouldBeHexagonsOnTheBoard(int expected)
        {
            Assert.AreEqual(expected, _board.GetHexagons().Count());
            Assert.AreEqual(expected, _board.GetEdges().SelectMany(e => e.Adjacent.Hexagons).Distinct().Count());
            Assert.AreEqual(expected, _board.GetVertices().SelectMany(e => e.Adjacent.Hexagons).Distinct().Count());
        }

        [Then(@"there should be (.*) vectices on the board")]
        public void ThenThereShouldBeVecticesOnTheBoard(int expected)
        {
            Assert.AreEqual(expected, _board.GetVertices().Count());
            Assert.AreEqual(expected, _board.GetEdges().SelectMany(e => e.Adjacent.Vertices).Distinct().Count());
            Assert.AreEqual(expected, _board.GetHexagons().SelectMany(e => e.Adjacent.Vertices).Distinct().Count());
        }

        [Then(@"there should be (.*) edges on the board")]
        public void ThenThereShouldBeEdgesOnTheBoard(int expected)
        {
            Assert.AreEqual(expected, _board.GetEdges().Count());
            Assert.AreEqual(expected, _board.GetVertices().SelectMany(e => e.Adjacent.Edges).Distinct().Count());
            Assert.AreEqual(expected, _board.GetHexagons().SelectMany(e => e.Adjacent.Edges).Distinct().Count());
        }

        [Then(@"the details of hexagons should be the following:")]
        public void ThenTheDetailsOfHexagonsShouldBeTheFollowing(Table table)
        {
            foreach (var row in table.Rows)
            {
                var hex = _board[int.Parse(row["X"]), int.Parse(row["Y"])];
                Assert.IsNotNull(hex);
                Assert.AreEqual(int.Parse(row["Number Marker"]), hex.ProductionNumber);
                Assert.AreEqual(row["Student"], hex.Degree.ToString());
                var adj = hex.Adjacent;
                Assert.AreEqual(int.Parse(row["Adj. # of hexes"]), adj.Hexagons.Count);
            }
        }

        [Then(@"the adjacent information should be the following:")]
        public void ThenTheAdjacentInformationShouldBeTheFollowing(Table table)
        {
            foreach (var row in table.Rows)
            {
                switch (row["Place Type"])
                {
                    case "Edge":
                        AssertAdjacentInfo(_board.GetEdges(), row);
                        break;
                    case "Vertex":
                        AssertAdjacentInfo(_board.GetVertices(), row);
                        break;
                    case "Hexagon":
                        AssertAdjacentInfo(_board.GetHexagons(), row);
                        break;
                    default:
                        throw new Exception("Unknown Place Type: " + row["Place Type"]);
                }
            }
        }

        private void AssertAdjacentInfo(IEnumerable<Place> places, TableRow row)
        {
            foreach (var place in places)
            {
                var adjacentInfo = place.Adjacent;
                Assert.GreaterOrEqual(adjacentInfo.Vertices.Count, int.Parse(row["Min# of vertices"]));
                Assert.LessOrEqual(adjacentInfo.Vertices.Count, int.Parse(row["Max# of vertices"]));
                Assert.GreaterOrEqual(adjacentInfo.Edges.Count, int.Parse(row["Min# of edges"]));
                Assert.LessOrEqual(adjacentInfo.Edges.Count, int.Parse(row["Max# of edges"]));
                Assert.GreaterOrEqual(adjacentInfo.Hexagons.Count, int.Parse(row["Min# of hexagons"]));
                Assert.LessOrEqual(adjacentInfo.Hexagons.Count, int.Parse(row["Max# of hexagons"]));
            }
        }


        [Then(@"the resource count should be the following:")]
        public void ThenTheResourceCountShouldBeTheFollowing(Table table)
        {
            var hexes = _board.GetHexagons();
            foreach (var row in table.Rows)
            {
                var type = (DegreeType)Enum.Parse(typeof (DegreeType), row["Resource"]);
                Assert.AreEqual(int.Parse(row["Count"]), hexes.Count(h => h.Degree == type));
            }
        }

        [Then(@"all edges should have 2 adjacent vertices")]
        public void ThenAllEdgesShouldHave2AdjacentVertices()
        {
            Assert.IsTrue(_board.GetEdges().All(e => e.Adjacent.Vertices.Count == 2));
        }

        [When(@"I set up the Catan beginner's game")]
        public void WhenISetUpTheCatanBeginnerSGame()
        {
            _game = new SettlerBeginnerGame();
        }

        [Then(@"the current game phase should be '(.*)'")]
        public void ThenTheCurrentGamePhaseShouldBe(string expected)
        {
            StringAssert.AreEqualIgnoringCase(expected, _game.CurrentPhase.ToString());
        }

        [Then(@"the player information should be the following:")]
        public void ThenThePlayerInformationShouldBeTheFollowing(Table table)
        {
            foreach (var row in table.Rows)
            {
                var player = _game.GetUniversityByColor((Color) Enum.Parse(typeof (Color), row["Player"]));
                Assert.AreEqual(int.Parse(row["Score"]), _game.GetScore(player));
            }
        }

    }
}
