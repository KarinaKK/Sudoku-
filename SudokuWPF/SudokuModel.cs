using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using SudokuRep;

namespace SudokuWPF
{
    public class SudokuModel
    {
        public SudokuModel()
        {
            Users = new ObservableCollection<User>
            {
                new User()
                {
                    Games = new ObservableCollection<Game>
                    {
                        new Game
                        {
                            Duration = 300,
                            Level = 2
                        },
                        new Game
                        {
                            Duration = 370,
                            Level = 1
                        },
                    },

                    login = "test"
                }
            };                        
        }

        public int Score(User user)
        { 
            
                int score = 0;
                foreach (var game in user.Games)
                {
                    score += CalculateScore(game.Level, game.Duration.GetValueOrDefault());
                }
                return score;            
        }

        public int GameCount(User user) => user.Games.Count;

        private int CalculateScore(int level, int time)
        {
            return (int)(level / (0.01 + (double)time / 3600));
        }
        public ObservableCollection<User> Users { get; set; }
    }
}
