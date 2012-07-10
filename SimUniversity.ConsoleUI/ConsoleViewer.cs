using System;
using MingStar.SimUniversity.Board;
using MingStar.SimUniversity.Contract;
using MingStar.SimUniversity.Game;
using MingStar.Utilities;

namespace MingStar.SimUniversity.ConsoleUI
{
    public class ConsoleViewer : IGameViewer
    {
        private Board.Board _board;
        private Game.Game _game;
        private int _maxXWithMaxY;
        private ConsolePixel[,] _printBuffer;

        #region IViewer Members

        public void PrintGame()
        {
            ColorConsole.WriteLine(ConsoleColor.Magenta,
                                   "\n============= Game Phase: {0} === Turn: {1} =============",
                                   _game.CurrentPhase,
                                   _game.CurrentTurn);
            UpdateBuffer();
            PrintBufferToBoard();
            PrintUniversityDetails();
            PrintStats();
        }

        public void PrintStats()
        {
            if (_game.CurrentPhase != GamePhase.Play)
            {
                PrintScarcity();
            }
            foreach (University uni in _game.Universities)
            {
                ConsoleColor uniColor = (uni == _game.CurrentIUniversity)
                                            ? ConsoleColor.DarkYellow
                                            : ConsoleColor.DarkCyan;
                ColorConsole.Write(uniColor, "Production: {0}Uni {1} [",
                                   (uni == _game.CurrentIUniversity) ? "CURRENT " : "",
                                   uni.Color
                    );
                int total = 0;
                foreach (var degree in GameConstants.RealDegrees)
                {
                    int degreeChance = uni.ProductionChances[degree];
                    if (degreeChance > 0)
                    {
                        ColorConsole.Write(uniColor, "{0}: {1} ", // ({2:#.##;;0}) ",
                                           degree,
                                           degreeChance //(int)GameConstants.Chance.TotalDiceRoll,
                            //degreeChance / GameConstants.Chance.TotalDiceRoll
                            );
                        total += degreeChance;
                    }
                }
                ColorConsole.WriteLine(uniColor, "] Total: {0}", total);
            }
        }

        public void PrintFinalResult(TimeSpan timeTaken)
        {
            Console.Title = string.Format("Round {0}, Winner: {1}!", _game.Round, _game.CurrentUniversityColor);
            PrintGame();
            ColorConsole.WriteLine(ConsoleColor.Magenta, "Round {0}, turn {1}: the game has a winner!", _game.Round,
                                   _game.CurrentTurn);
            ColorConsole.WriteLine(ConsoleColor.Green, "Total time taken: {0}", timeTaken);
            _game.GameStats.PrintDiceRolls();
        }


        public void PrintTitle()
        {
            Console.Title = string.Format("Round {0}, Turn: {1}", _game.Round, _game.CurrentTurn);
        }


        public void PrintLegalMove(IPlayerMove move)
        {
            ColorConsole.WriteLineIf(_game.HasHumanPlayer, ConsoleViewerColor.Move, move);
        }

        public void PrintIllegalMove(IPlayerMove move)
        {
            ColorConsole.WriteLine(ConsoleColor.Red,
                                   "Illegal Move '{0}' for university {1}",
                                   move,
                                   _game.CurrentUniversity);
        }


        public void SetGame(Game.Game game)
        {
            _game = game;
            _board = game.Board;
            InitialiseBuffer();
        }

        #endregion

        /*
         * prints the board to console as something like:
         * 
                      .-.
                     /12 \
                  .-. 0,1 .
                 / 6 \ S /
                . 0,0 .-. 
                 \ S / 3 \
                  .-. 1,0 .
                     \ G /
                      T-*          
         */

        private void InitialiseBuffer()
        {
            // find out whether even X is at the top
            _maxXWithMaxY = _board.MinX;
            foreach (Hexagon hex in _board.GetHexagons())
            {
                Position pos = hex.Position;
                if (pos.Y == _board.MaxY)
                {
                    _maxXWithMaxY = Math.Max(pos.X, _maxXWithMaxY);
                }
            }
            //
            int deltaY = _board.MaxY - _board.MinY + 1;
            int deltaX = _board.MaxX - _board.MinX + 1;
            int maxY = deltaY*4 + 3;
            int maxX = deltaX*4 + 3;
            _printBuffer = new ConsolePixel[maxY,maxX];
        }

        private Position HexToConsole(Position hexPos)
        {
            int x = (hexPos.X - _board.MinX)*4 + 3;
            int y = (_board.MaxY - hexPos.Y)*4 + 3 - hexPos.X*2;
            return new Position(x, y);
        }

        public void PrintUniversityDetails()
        {
            foreach (University uni in _game.Universities)
            {
                if (uni == _game.CurrentUniversity)
                {
                    ColorConsole.WriteLine(ConsoleColor.Yellow,
                                           "CURRENT: {0}", _game.CurrentUniversity);
                }
                else
                {
                    ColorConsole.WriteLine(ConsoleColor.Cyan, uni);
                }
            }
        }

        private void PrintScarcity()
        {
            const ConsoleColor color = ConsoleColor.DarkGreen;
            ColorConsole.Write(color, "Degree Scarcity: ");
            foreach (DegreeType degree in GameConstants.RealDegrees)
            {
                ColorConsole.Write(color, "{0}: {1:#.##} ", degree, _game.Scarcity[degree]);
            }
            Console.WriteLine();
        }

        private void UpdateBuffer()
        {
            foreach (var hex in _board.GetHexagons())
            {
                UpdateBuffer(hex);
            }
            foreach (var vertex in _board.GetVertices())
            {
                UpdateBuffer(vertex);
            }
            foreach (var edge in _board.GetEdges())
            {
                UpdateBuffer(edge);
            }
        }

        private void UpdateBuffer(Hexagon hex)
        {
            var pos = HexToConsole(hex.Position);
            var defaultForeColor = (hex.ProductionNumber == 0)
                                                ? ConsolePixel.DefaultForeColor
                                                : ConsoleViewerColor.Degree[hex.Degree];
            PrintNumber(hex.ProductionNumber, pos.Y - 1, pos.X, true, defaultForeColor);
            _printBuffer[pos.Y, pos.X] = new ConsolePixel(',')
                                             {
                                                 ForeColor = defaultForeColor,
                                             };
            printNumberNegRed(hex.Position.X, pos.Y, pos.X - 1, true, defaultForeColor);
            printNumberNegRed(hex.Position.Y, pos.Y, pos.X + 1, false, defaultForeColor);
            PrintNumber(GameConstants.HexID2Chance[hex.ProductionNumber], pos.Y + 1, pos.X, true,
                        ConsoleColor.DarkMagenta);
        }

        private void PrintNumber(int number, int bufferY, int bufferX, bool rightAligned, ConsoleColor foreColor)
        {
            bool negative = number < 0;
            number = Math.Abs(number);
            char numChar = (number != 0) ? (char) ((number%10) + '0') : 'X';
            _printBuffer[bufferY, bufferX] = new ConsolePixel(numChar)
                                                 {
                                                     ForeColor = foreColor
                                                 };
            if (number >= 10 || negative)
            {
                int deltaX = rightAligned ? -1 : 1;
                char c = negative ? '-' : (char) ((number/10) + '0');
                _printBuffer[bufferY, bufferX + deltaX] = new ConsolePixel(c)
                                                              {
                                                                  ForeColor = foreColor
                                                              };
            }
        }

        private void printNumberNegRed(int number, int bufferY, int bufferX, bool rightAligned,
                                       ConsoleColor defaultForeColor)
        {
            int deltaX = rightAligned ? -1 : 1;
            ConsoleColor foreColor = number < 0
                                         ? ConsoleColor.Red
                                         : defaultForeColor;
            number = Math.Abs(number);
            _printBuffer[bufferY, bufferX] =
                new ConsolePixel
                    {
                        ForeColor = foreColor,
                        Char = (char) ((number%10) + '0')
                    };
            if (number >= 10)
            {
                _printBuffer[bufferY, bufferX + deltaX] =
                    new ConsolePixel
                        {
                            ForeColor = foreColor,
                            Char = (char) ((number/10) + '0')
                        };
            }
        }


        private void UpdateBuffer(Edge edge)
        {
            var pos = HexToConsole(edge.Position.HexPosition);
            char value = ' ';
            switch (edge.Position.Orientation)
            {
                case EdgeOrientation.TopLeft:
                    pos = pos.Add(-2, -1);
                    value = '/';
                    break;
                case EdgeOrientation.BottomLeft:
                    pos = pos.Add(-2, 1);
                    value = '\\';
                    break;
                case EdgeOrientation.Bottom:
                    pos = pos.Add(0, 2);
                    value = '-';
                    break;
                case EdgeOrientation.BottomRight:
                    pos = pos.Add(2, 1);
                    value = '/';
                    break;
                case EdgeOrientation.TopRight:
                    pos = pos.Add(2, -1);
                    value = '\\';
                    break;
                case EdgeOrientation.Top:
                    pos = pos.Add(0, -2);
                    value = '-';
                    break;
            }
            _printBuffer[pos.Y, pos.X] = ConsolePixel.GetEdgePixel(edge, value);
        }

        private void UpdateBuffer(Vertex vertex)
        {
            Position pos = HexToConsole(vertex.Position.HexPosition);
            switch (vertex.Position.Orientation)
            {
                case VertexOrientation.TopLeft:
                    pos = pos.Add(-1, -2);
                    break;
                case VertexOrientation.Left:
                    pos = pos.Add(-3, 0);
                    break;
                case VertexOrientation.BottomLeft:
                    pos = pos.Add(-1, 2);
                    break;
                case VertexOrientation.BottomRight:
                    pos = pos.Add(1, 2);
                    break;
                case VertexOrientation.Right:
                    pos = pos.Add(3, 0);
                    break;
                case VertexOrientation.TopRight:
                    pos = pos.Add(1, -2);
                    break;
            }
            _printBuffer[pos.Y, pos.X] = ConsolePixel.GetVertexPixel(vertex);
        }

        private void PrintBufferToBoard()
        {
            for (int y = 0; y < _printBuffer.GetLength(0); ++y)
            {
                for (int x = 0; x < _printBuffer.GetLength(1); ++x)
                {
                    ConsolePixel pixel = _printBuffer[y, x] ?? ConsolePixel.EmptyPixel;
                    var previousForeColor = Console.ForegroundColor;
                    var previousBackColor = Console.BackgroundColor;
                    Console.ForegroundColor = pixel.ForeColor;
                    Console.BackgroundColor = pixel.BackColor;
                    Console.Write(pixel.Char);
                    Console.ForegroundColor = previousForeColor;
                    Console.BackgroundColor = previousBackColor;
                }
                Console.WriteLine();
            }
        }
    }
}