using System;
using System.Collections.Generic;
using System.Linq;
using MingStar.SimUniversity.Board;
using MingStar.SimUniversity.Board.Constructor;
using MingStar.SimUniversity.Contract;
using MingStar.SimUniversity.Game;
using MingStar.SimUniversity.Game.Games;
using MingStar.SimUniversity.Tests.Mocks;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace MingStar.SimUniversity.Tests
{
    [Binding]
    public class StepsDefinition
    {
        private static FakeDiceTotalGenerator _fakeDiceRoll;
        private static Game.Game _game;
        private Board.Board _board;

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            _fakeDiceRoll = new FakeDiceTotalGenerator();
            Dice.DiceTotalGenerator = _fakeDiceRoll;
        }

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

        [When(@"the dice roll is predefined to (.*)")]
        public void WhenTheDiceRollIsPredefinedTo(int number)
        {
            _fakeDiceRoll.SetNextRoll(number);
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
            foreach (TableRow row in table.Rows)
            {
                Hexagon hex = _board[int.Parse(row["X"]), int.Parse(row["Y"])];
                Assert.IsNotNull(hex);
                Assert.AreEqual(int.Parse(row["Number Marker"]), hex.ProductionNumber);
                Assert.AreEqual(row["Student"], hex.Degree.ToString());
                AdjacentInfo adj = hex.Adjacent;
                Assert.AreEqual(int.Parse(row["Adj. # of hexes"]), adj.Hexagons.Count);
            }
        }

        [Then(@"the adjacent information should be the following:")]
        public void ThenTheAdjacentInformationShouldBeTheFollowing(Table table)
        {
            foreach (TableRow row in table.Rows)
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
            foreach (Place place in places)
            {
                AdjacentInfo adjacentInfo = place.Adjacent;
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
            Hexagon[] hexes = _board.GetHexagons();
            foreach (TableRow row in table.Rows)
            {
                var type = (DegreeType) Enum.Parse(typeof (DegreeType), row["Resource"]);
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

        [Then(@"the university information should be the following:")]
        public void ThenTheUniversityInformationShouldBeTheFollowing(Table table)
        {
            foreach (TableRow row in table.Rows)
            {
                University university = _game.GetUniversityByColor(ParseColor(row["University"]));
                if (row.ContainsKey("Score"))
                {
                    Assert.AreEqual(int.Parse(row["Score"]), _game.GetScore(university));
                }
                if (row.ContainsKey("Campuses"))
                {
                    IEnumerable<Vertex> expectedVertices =
                        row["Campuses"].Split(';').Select(l => _game.Board[ParseVertexPosition(l)]);
                    CollectionAssert.AreEquivalent(university.Campuses, expectedVertices);
                }
                if (row.ContainsKey("Links"))
                {
                    IEnumerable<Edge> expectedEdges =
                        row["Links"].Split(';').Select(l => _game.Board[ParseEdgePosition(l)]);
                    CollectionAssert.AreEquivalent(university.InternetLinks, expectedEdges);
                }
                if (row.ContainsKey("Students"))
                {
                    DegreeCount expectedStudents = ParseStudents(row["Students"]);
                    Assert.AreEqual(expectedStudents, university.Students);
                }
            }
        }

        [When(@"the enrolment happens with dice roll (.*)")]
        public void WhenTheEnrolmentHappensWithDiceRoll(int roll)
        {
            Assert.LessOrEqual(roll, 12);
            Assert.GreaterOrEqual(roll, 2);
            //_game.EndTurn(roll);
        }


        private DegreeCount ParseStudents(string str)
        {
            IEnumerable<Tuple<DegreeType, int>> degrees = str.Split(',').Select(s => ParseStudent(s.Trim()));
            var count = new DegreeCount();
            foreach (var degree in degrees)
            {
                count[degree.Item1] += degree.Item2;
            }
            return count;
        }

        private Tuple<DegreeType, int> ParseStudent(string str)
        {
            DegreeType degree;
            switch (str)
            {
                case "b":
                    degree = DegreeType.Brick;
                    break;
                case "w":
                    degree = DegreeType.Wood;
                    break;
                case "o":
                    degree = DegreeType.Ore;
                    break;
                case "g":
                    degree = DegreeType.Grain;
                    break;
                case "s":
                    degree = DegreeType.Sheep;
                    break;
                default:
                    return new Tuple<DegreeType, int>(DegreeType.None, 0);
            }
            return new Tuple<DegreeType, int>(degree, 1);
        }

        private static Color ParseColor(string color)
        {
            return (Color) Enum.Parse(typeof (Color), color);
        }

        private EdgePosition ParseEdgePosition(string str)
        {
            string[] items = str.Trim('(', ')', ' ').Split(',').Select(s => s.Trim()).ToArray();
            return new EdgePosition(new Position(int.Parse(items[0]), int.Parse(items[1])), GetEdgeOrientation(items[2]));
        }

        private VertexPosition ParseVertexPosition(string str)
        {
            string[] items = str.Trim('(', ')', ' ').Split(',').Select(s => s.Trim()).ToArray();
            return new VertexPosition(new Position(int.Parse(items[0]), int.Parse(items[1])),
                                      GetVertexOrientation(items[2]));
        }

        private VertexOrientation GetVertexOrientation(string str)
        {
            switch (str.ToLower())
            {
                case "r":
                    return VertexOrientation.Right;
                case "tr":
                    return VertexOrientation.TopRight;
                case "tl":
                    return VertexOrientation.TopLeft;
                case "l":
                    return VertexOrientation.Left;
                case "bl":
                    return VertexOrientation.BottomLeft;
                case "br":
                    return VertexOrientation.BottomRight;
                default:
                    throw new Exception("Unknown vertex orientation: " + str);
            }
        }

        private EdgeOrientation GetEdgeOrientation(string str)
        {
            switch (str.ToLower())
            {
                case "t":
                    return EdgeOrientation.Top;
                case "tr":
                    return EdgeOrientation.TopRight;
                case "tl":
                    return EdgeOrientation.TopLeft;
                case "b":
                    return EdgeOrientation.Bottom;
                case "bl":
                    return EdgeOrientation.BottomLeft;
                case "br":
                    return EdgeOrientation.BottomRight;
                default:
                    throw new Exception("Unknown vertex orientation: " + str);
            }
        }

        [Then(@"the current university of the turn should be '(.*)'")]
        public void ThenTheCurrentUniversityOfTheTurnShouldBeRed(string expected)
        {
            Assert.AreEqual(ParseColor(expected), _game.CurrentUniversityColor);
        }
    }
}