using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Media;
using SudokuRep;

namespace SudokuWPF
{
    
    public delegate void AuthChangedHandle(User user);
    public class SudokuModel:IUserCredentials,IUserData
    {

       
        private User _authUser;

        private Game _newGame;

        private int[][] _gameBoardFull;
        private int?[][] _gameBoard;
        private UserDataRepository _userDataRepository;
        private UserCredentialsRepository _userCredentialsRepository;

        // Объявляем событие
        public event AuthChangedHandle AuthChanged;

        public void StartGame(int level)
        {
            _gameBoardFull = SudokuGenerator.GenerateSudoku();
            SetupBoard(level);
            NewGame=new Game
            {
                StartTime = DateTime.Now,
                Level = level,
                User = AuthUser
            };
            
        }

        public void FinishGame()
        {
            NewGame.FinishTime = DateTime.Now;
            NewGame.duration = (NewGame.FinishTime.Ticks - NewGame.StartTime.Ticks)*1000000;
            AddOrUpdateGame(NewGame);
            NewGame = null;
            
        }
        public SudokuModel()
        {
            _gameBoard = new int?[9][];
            Raiting=new List<RatingModel>();
            SudokuUsersEntities enteties=new SudokuUsersEntities();
            _userDataRepository = new UserDataRepository(enteties);
            _userCredentialsRepository=new UserCredentialsRepository(enteties);
        }
        private void SetupBoard(int level)
        {
           
            for (int i = 0; i < 9; i++)
            {
                _gameBoard[i] = new int?[9];
                for (int j = 0; j < 9; j++)
                    _gameBoard[i][j] = _gameBoardFull[i][j];
            }

            int remainingItems = ((int)level + 1) * 20;
            while (remainingItems > 0)
            {
                int posX = SudokuGenerator.Rand.Next(9);
                int posY = SudokuGenerator.Rand.Next(9);
                if (_gameBoard[posX][posY] == null)
                    continue;
                _gameBoard[posX][posY] = null;
                remainingItems--;
            }
        }

        public User AuthUser
        {
            get { return _authUser; }
            set
            {
                AuthChanged?.Invoke(value);
                _authUser = value;
            }
        }


        public Game NewGame
        {
            get => _newGame;
            set => _newGame = value;
        }

        public void UpdateRaiting()
        {
            foreach (var user in _userDataRepository.GetUsers())
            {
                Raiting.Add(new RatingModel
                {
                    Count = user.Games.Count,
                    login = user.login,
                    Score = user.Games.Sum(g=>g.Level*(int)(1/g.duration.GetValueOrDefault()))
                });
            }   
        }
        public List<RatingModel> Raiting { get; set; }
        
        public int?[][] GameBoard { get => _gameBoard; set => _gameBoard = value; }
        public int[][] GameBoardFull { get => _gameBoardFull; set => _gameBoardFull = value; }

        public void AddOrUpdateGame(Game game)
        {
            _userDataRepository.AddOrUpdateGame(game);
        }

        public IQueryable GetRating()
        {
            return _userDataRepository.GetRating();
        }

        public void DeleteGame(Game game)
        {
            _userDataRepository.DeleteGame(game);
        }

        public void AddOrUpdateUser(User user)
        {
            ((IUserCredentials)_userCredentialsRepository).AddOrUpdateUser(user);
        }

        public bool CheckUserByName(User user)
        {
            return ((IUserCredentials)_userCredentialsRepository).CheckUserByName(user);
        }

        public string GetUserPassByLogin(string login)
        {
            return ((IUserCredentials)_userCredentialsRepository).GetUserPassByLogin(login);
        }

        public User GetUserByCredentials(string login, string hashpassword)
        {
            return ((IUserCredentials)_userCredentialsRepository).GetUserByCredentials(login, hashpassword);
        }
    }
}
