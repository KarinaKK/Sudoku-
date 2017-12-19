using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using SudokuWPF;

namespace SudokuRep
{
    public class UserDataRepository:IUserData
    {
        private SudokuUsersEntities _dbUsersEntities;
        public UserDataRepository(SudokuUsersEntities entities)
        {
            _dbUsersEntities = entities;
        }

        public List<User> GetUsers()
        {
            return _dbUsersEntities.Users.ToList();
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
                    login=total.login,
                    Count=total.Games.Count,
                    Score = total.Games.Sum(g=>g.Level*(1/(g.duration)))
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
