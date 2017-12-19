using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace SudokuRep
{
    public class UserDataRepository
    {
        private SudokuUsersEntities _dbUsersEntities;
        public UserDataRepository()
        {            
            _dbUsersEntities=new SudokuUsersEntities();
        }

        public void AddOrUpdateGame(Game game)
        {
            _dbUsersEntities.Games.Add(game);
            _dbUsersEntities.SaveChanges();
        }

        public IQueryable GetRating()
        {
            return _dbUsersEntities.Users.OrderByDescending(
                x => x.Games.Sum(g => g.Level)).Select(
                total => new
                {
                    total.login,
                    total.Games.Count,
                    Score = total.Games.Sum(g=>g.Level)
                }
            );

        }
        public void DeleteGame(Game game)
        {
            _dbUsersEntities.Games.Remove(game);
            _dbUsersEntities.SaveChanges();
        }

    }
}
