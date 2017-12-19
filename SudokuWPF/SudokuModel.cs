using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using SudokuRep;

namespace SudokuWPF
{
    
    public delegate void AuthChangedHandle(User user);
    public class SudokuModel
    {

       
        private User _authUser;

        private Game _newGame;

        private int[][] _gameBoardFull;
        private int?[][] _gameBoard;
        private UserDataRepository _userDataRepository;

        // Объявляем событие
        public event AuthChangedHandle AuthChanged;

        public void StartGame(int level)
        {
            _gameBoardFull = SudokuGenerator.GenerateSudoku();
            SetupBoard(level);
            NewGame=new Game
            {
                Level = level,
                User = AuthUser
            };
            
        }
        public SudokuModel()
        {
            _gameBoard = new int?[9][];
            _userDataRepository = new UserDataRepository();
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
        public ObservableCollection<User> Users { get; set; }

        public Game NewGame
        {
            get { return _newGame; }
            set { _newGame = value; }
        }

        public void UpdateRaiting()
        {            
            Raiting = _userDataRepository.GetRating();
        }
        public IQueryable Raiting { get; set; }
        public void GameOver()
        {
            NewGame = null;
            _gameBoard = null;
            _gameBoardFull = null;            
        }
        public int?[][] GameBoard { get { return _gameBoard; } set { _gameBoard = value; } }
        public int[][] GameBoardFull { get { return _gameBoardFull; } set { _gameBoardFull = value; } }

        public void AddUserData()
        {
            _userDataRepository.AddOrUpdateGame(NewGame);
        }
    }
}
