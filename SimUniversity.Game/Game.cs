using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using MingStar.SimUniversity.Board;
using MingStar.SimUniversity.Contract;
using MingStar.SimUniversity.Game.Move;
using MingStar.SimUniversity.Game.Random;
using MingStar.SimUniversity.Game.Rules;
using MingStar.Utilities;

namespace MingStar.SimUniversity.Game
{
    public class Game : IGame
    {
        #region Non-Public Fields

        private readonly Dictionary<IVertex, int> _vertexProductionChances = new Dictionary<IVertex, int>();
        internal ReadOnlyCollection<IPlayerMove> AllMoves;
        private Dictionary<Color, University> _color2University;
        private int _currentUniversityIndex;
        private Stack<TurnInfo> _previousTurnInfo;
        private SetupPhraseMoveGenerator _setupMoveGenerator;
        private University[] _universities;
        internal IZobristHashing Hashing { get; private set; }

        #endregion

        #region Public Properties

        public int Round { get; set; }
        public bool HasHumanPlayer { get; set; }
        public static IRandomEvent RandomEventChance { get; set; }
        #endregion

        #region Public Queriable Properties

        public int NumberOfUniversities { get; private set; }
        public double ProbabilityWithNoCut { get; private set; }
        public int CurrentTurn { get; protected set; }
        public IBoard IBoard { get { return Board; } }
        public Board.Board Board { get; private set; }
        public IMostInfo MostFailedStartUps { get; internal set; }
        public IMostInfo LongestInternetLink { get; internal set; }
        public IGameRules Rules { get; private set; }
        public GameStats GameStats { get; private set; }

        public int CurrentUniversityIndex
        {
            get { return _currentUniversityIndex; }
            private set
            {
                if (_currentUniversityIndex != value)
                {
                    Hashing.HashCurrentUniversity(CurrentUniversityColor);
                    _currentUniversityIndex = value;
                    Hashing.HashCurrentUniversity(CurrentUniversityColor);
                }
            }
        }

        public IUniversity CurrentIUniversity
        {
            get { return _universities[CurrentUniversityIndex]; }
        }

        public University CurrentUniversity
        {
            get { return _universities[CurrentUniversityIndex]; }
        }

        public ReadOnlyCollection<IUniversity> Universities
        {
            get
            {
                return _universities.ToList<IUniversity>().AsReadOnly();
            }
        }

        public ulong Hash
        {
            get { return Hashing.Hash; }
            internal set { Hashing.Hash = value; }
        }

        public GamePhase CurrentPhase { get; private set; }

        public Color CurrentUniversityColor
        {
            get { return CurrentUniversity.Color; }
        }

        #endregion

        #region Constructor

        public Game(IBoard board, int numOfPlayers)
            : this(board, numOfPlayers, new NormalRules())
        {
        }

        public Game(IBoard board, int numOfPlayers, IGameRules rules)
        {
            Board = (Board.Board)board; //FIXME: remove type casting
            Rules = rules;
            NumberOfUniversities = numOfPlayers;
            ProbabilityWithNoCut = Math.Pow(
                (1 - GameConstants.HexID2Chance[7]/GameConstants.Chance.TotalDiceRoll),
                NumberOfUniversities
                );
            CreateUniversities(numOfPlayers);
            Round = 1;
            Reset();
        }

        #endregion

        #region Non-Public Methods

        public bool IsLegalToBuildLink(EdgePosition pos)
        {
            var edge = Board[pos];
            if (edge == null || edge.Color != null)
            {
                return false;
            }
            if (CurrentPhase == GamePhase.Play)
            {
                return edge.Adjacent.Edges.Any(e => e.Color == CurrentUniversityColor);
            }
            else
            {
                // links to own campus
                var vertex =
                    edge.Adjacent.Vertices.SingleOrDefault(
                        v => v.Campus != null && v.Campus.Color == CurrentUniversityColor);
                if (vertex == null)
                {
                    return false;
                }
                // but not the same campus one as the other link                
                return !edge.Adjacent.Edges.Any(e => e.Color == CurrentUniversityColor &&
                                                     e.Adjacent.Vertices.Contains(vertex));
            }
        }

        public bool IsLegalToBuildCampus(VertexPosition whereAt, CampusType type)
        {
            var vertex = Board[whereAt];
            if (vertex == null)
            {
                return false;
            }
            if (type == CampusType.Super)
            {
                return (vertex.Campus != null &&
                        vertex.Campus.Color == CurrentUniversityColor &&
                        vertex.Campus.Type == CampusType.Traditional);
            }
            else //if CampusType.Traditional
            {
                bool result = vertex.IsFreeToBuildCampus();
                if (CurrentPhase == GamePhase.Play)
                {
                    result = result && vertex.Adjacent.Edges.Any(e => e.Color == CurrentUniversityColor);
                }
                return result;
            }
        }

        private void CreateUniversities(int numOfPlayers)
        {
            _universities = new University[numOfPlayers];
            _color2University = new Dictionary<Color, University>();
            for (int i = 0; i < numOfPlayers; ++i)
            {
                var color = (Color) i;
                var uni = new University(this, color, i);
                _universities[i] = uni;
                _color2University[color] = uni;
            }
        }

        private DegreeCount[] Enrol(int hexID)
        {
            var array = new DegreeCount[NumberOfUniversities];
            int index, number;
            foreach (Hexagon hex in Board[hexID])
            {
                foreach (Vertex vertex in hex.Adjacent.Vertices)
                {
                    if (vertex.Campus != null)
                    {
                        University uni = _color2University[vertex.Campus.Color];
                        index = uni.PlayerIndex;
                        number = (vertex.Campus.Type == CampusType.Traditional) ? 1 : 2;
                        if (array[index] == null)
                        {
                            array[index] = new DegreeCount();
                        }
                        array[index][hex.Degree] += number;
                        uni.EnrolStudents(hex.Degree, number);
                    }
                }
            }
            return array;
        }


        private DegreeType[][] CutStudents()
        {
            var dict = new DegreeType[NumberOfUniversities][];
            for (int i = 0; i < NumberOfUniversities; ++i)
            {
                dict[i] = _universities[i].CutStudents();
            }
            return dict;
        }

        #endregion

        #region Public Methods

        public void BuildCampus(VertexPosition whereAt, CampusType type)
        {
            var vertex = Board[whereAt];
            Board.BuildCampus(vertex, type, CurrentUniversity.Color);
            CurrentUniversity.AddCampus(vertex, type);
        }

        public void UndoBuildCampus(VertexPosition whereAt)
        {
            var vertex = Board[whereAt];
            CurrentUniversity.RemoveCampus(vertex);
            Board.UnBuildCampus(whereAt);
        }

        public void BuildLink(EdgePosition whereAt)
        {
            var edge = Board[whereAt];
            Board.BuildLink(edge, CurrentUniversity.Color);
            CurrentUniversity.AddLink(edge);
            Hashing.HashEdge(CurrentUniversityColor, edge.Position);
            if (CurrentPhase == GamePhase.Play)
            {
                Hashing.HashMostInfo(LongestInternetLink.University, MostInfoType.Links);
                LongestInternetLink =
                    LongestInternetLink.GetMore(CurrentUniversity,
                                                CurrentUniversity.LengthOfLongestLink);
                Hashing.HashMostInfo(LongestInternetLink.University, MostInfoType.Links);
            }
        }

        public void UndoBuildLink(EdgePosition whereAt)
        {
            Board.UnBuildLink(whereAt);
            CurrentUniversity.RemoveLink(Board[whereAt]);
        }

        public void UndoEndTurn(int diceTotal, EnrolmentInfo enrolmentInfo)
        {
            if (enrolmentInfo.AddedStudents != null)
            {
                for (int i = 0; i < NumberOfUniversities; ++i)
                {
                    _universities[i].RemoveExtraStudents(enrolmentInfo.AddedStudents[i]);
                }
            }
            if (enrolmentInfo.CutStudents != null)
            {
                for (int i = 0; i < NumberOfUniversities; ++i)
                {
                    _universities[i].AddBackStudents(enrolmentInfo.CutStudents[i]);
                }
            }
            CurrentUniversityIndex = (CurrentUniversityIndex + _universities.Length - 1)%_universities.Length;
            --CurrentTurn;
            GameStats.UndoDiceRolled(diceTotal);
        }

        public EnrolmentInfo DiceRoll(int diceTotal)
        {
            if (CurrentPhase == GamePhase.Play)
            {
                GameStats.DiceRolled(diceTotal);
                return RollDice(diceTotal);
            }
            return null; // setup phase
        }

        public University GetUniversityByColor(Color color)
        {
            return _color2University[color];
        }

        protected void BuildCampus(int x, int y, VertexOrientation vo, CampusType type)
        {
            var whereAt = new VertexPosition(new Position(x, y), vo);
            BuildCampus(whereAt, type);
        }

        private void Reset()
        {
            Board.Clear();
            CurrentPhase = GamePhase.Setup1;
            CurrentTurn = 1;
            MostFailedStartUps = new MostInfo(3);
            LongestInternetLink = new MostInfo(5);
            AllMoves = null;
            _previousTurnInfo = new Stack<TurnInfo>();
            _setupMoveGenerator = new SetupPhraseMoveGenerator();
            GameStats = new GameStats();

            // special order
            Hashing = new NullHashing(this);
            foreach (University uni in _universities)
            {
                uni.Reset();
            }
            CurrentUniversityIndex = 0;
        }

        public void SetUpLink(int x, int y, EdgeOrientation eo)
        {
            BuildLink(new EdgePosition(x, y, eo));
        }

        public void NextTurn()
        {
            ++CurrentTurn;
            switch (CurrentPhase)
            {
                case GamePhase.Setup1:
                    if (CurrentTurn <= NumberOfUniversities)
                    {
                        CurrentUniversityIndex = (CurrentUniversityIndex + 1)%_universities.Length;
                    }
                    else
                    {
                        CurrentPhase = GamePhase.Setup2;
                    }
                    break;
                case GamePhase.Setup2:
                    if (CurrentTurn <= NumberOfUniversities*2)
                    {
                        CurrentUniversityIndex = (CurrentUniversityIndex - 1)%_universities.Length;
                    }
                    else
                    {
                        CurrentPhase = GamePhase.Play;
                        CurrentTurn = 1;
                    }
                    break;
                case GamePhase.Play:
                    CurrentUniversityIndex = (CurrentUniversityIndex + 1)%_universities.Length;
                    break;
            }
        }

        private EnrolmentInfo RollDice(int total)
        {
            ColorConsole.WriteLineIf(HasHumanPlayer, ConsoleColor.Green, "Dice rolled, total: {0}", total);
            var info = new EnrolmentInfo();
            if (total != 7)
            {
                info.AddedStudents = Enrol(total);
            }
            else
            {
                ColorConsole.WriteLineIf(HasHumanPlayer, ConsoleColor.Yellow, "Funds cut!");
                info.CutStudents = CutStudents();
            }
            return info;
        }

        #endregion

        private Dictionary<DegreeType, double> _scarcity;

        public Dictionary<DegreeType, double> Scarcity
        {
            get
            {
                if (_scarcity == null)
                {
                    _scarcity = new Dictionary<DegreeType, double>();
                    var hexCount = new DegreeCount();
                    var productionChance = new DegreeCount();
                    foreach (Hexagon hex in Board.GetHexagons())
                    {
                        productionChance[hex.Degree] += GameConstants.HexID2Chance[hex.ProductionNumber];
                        hexCount[hex.Degree] += 1;
                    }
                    foreach (DegreeType degree in productionChance.Keys)
                    {
                        _scarcity[degree] = GameConstants.Chance.SingleHexagonMax
                                            *hexCount[degree]
                                            /(double) productionChance[degree];
                    }
                }
                return _scarcity;
            }
        }

        #region IGame Members

        public bool HasWinner()
        {
            return GetScore(CurrentUniversity) >= Rules.WinningScore;
        }

        public bool IsLegalMove(IPlayerMove move)
        {
            if (move is RandomMove)
            {
                return true;
            }
            if (CurrentPhase == GamePhase.Play && !CurrentUniversity.HasStudentsFor(move.StudentsNeeded))
            {
                return false;
            }
            return move.IsLegalToApply(this);
        }

        public void ApplyMove(IPlayerMove move)
        {
            _previousTurnInfo.Push(TurnInfo.Create(this, move));
            move.ApplyTo(this);
            if (CurrentPhase == GamePhase.Play)
            {
                CurrentUniversity.ConsumeStudents(move);
            }
            if (CurrentPhase == GamePhase.Setup2 && move is BuildCampusMove)
            {
                CurrentUniversity.AcquireInitialStudents(((BuildCampusMove)move).WhereAt);
            }
            if (CurrentPhase != GamePhase.Play && !(move is EndTurn)) // setup phase
            {
                if (!_setupMoveGenerator.ShouldBuildCampus)
                {
                    ApplyMove(new EndTurn());
                }
                _setupMoveGenerator.Toggle();
            }
        }

        public void UndoMove()
        {
            TurnInfo turnInfo = _previousTurnInfo.Pop();
            AllMoves = turnInfo.AllMoves;
            MostFailedStartUps = turnInfo.MostFailedStartUps;
            LongestInternetLink = turnInfo.MostInternetLinks;
            Hash = turnInfo.Hash;
            if (CurrentPhase == GamePhase.Play)
            {
                CurrentUniversity.ResumeStudents(turnInfo.Move);
            }
            // NB. this has to be in the really end
            turnInfo.Move.Undo(this);
            CurrentUniversity.LengthOfLongestLink = turnInfo.CurrentUnversityLongestLink;
        }

        public ReadOnlyCollection<IPlayerMove> GenerateAllMoves()
        {
            if (CurrentPhase == GamePhase.Play)
            {
                AllMoves = PlayPhraseMoveGenerator.GenerateAllMoves(this).AsReadOnly();
            }
            else // GamePhase.Setup1 or 2
            {
                AllMoves = _setupMoveGenerator.GenerateAllMoves(this).AsReadOnly();
            }
            return AllMoves;
        }

        public void TradeInStudent(DegreeType tradedIn)
        {
            CurrentUniversity.EnrolStudents(tradedIn, 1);
        }

        public void UnTradeInStudent(DegreeType tradedIn)
        {
            CurrentUniversity.UntradeInStudents(tradedIn);
        }

        public void TryStartUp(bool isSuccessful)
        {
            Hashing.HashStartUp(CurrentUniversityColor, isSuccessful,
                                CurrentUniversity.NumberOfSuccessfulCompanies);
            if (isSuccessful)
            {
                ++CurrentUniversity.NumberOfSuccessfulCompanies;
            }
            else // failed
            {
                //TODO: steal card from other players
                ++CurrentUniversity.NumberOfFailedCompanies;
                Hashing.HashMostInfo(MostFailedStartUps.University, MostInfoType.FailedStartUps);
                MostFailedStartUps = MostFailedStartUps.GetMore(CurrentUniversity,
                                                                CurrentUniversity.NumberOfFailedCompanies);
                Hashing.HashMostInfo(MostFailedStartUps.University, MostInfoType.FailedStartUps);
            }
            Hashing.HashStartUp(CurrentUniversityColor, isSuccessful,
                                CurrentUniversity.NumberOfSuccessfulCompanies);
        }

        public void UndoTryStartUp(bool isSuccessful)
        {
            if (isSuccessful)
            {
                --CurrentUniversity.NumberOfSuccessfulCompanies;
            }
            else
            {
                --CurrentUniversity.NumberOfFailedCompanies;
            }
        }

        #endregion

        public int GetScore(IUniversity university)
        {
            int score = university.SuperCampuses.Count*2 +
                        university.Campuses.Count +
                        university.NumberOfSuccessfulCompanies;
            if (MostFailedStartUps.University == university)
            {
                score += GameConstants.Score.MostFailedStartUps;
            }
            if (LongestInternetLink.University == university)
            {
                score += GameConstants.Score.LongestInternetLinks;
            }
            return score;
        }

        public int GetVertexProductionChance(IVertex vertex)
        {
            if (!_vertexProductionChances.ContainsKey(vertex))
            {
                int chance = 0;
                foreach (var hex in vertex.Adjacent.Hexagons)
                {
                    chance += GameConstants.HexID2Chance[hex.ProductionNumber];
                }
                _vertexProductionChances[vertex] = chance;
            }
            return _vertexProductionChances[vertex];
        }
    }
}