using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        //public List<User> GetLeadersBoard()
        //{
        //    _dbUsersEntities.Users.OrderByDescending(
        //        u=>u.Games.Sum(
        //            g=>1/g.Duration.GetValueOrDefault())*
                    
        //            )
        //}
        public void DeleteGame(Game game)
        {
            _dbUsersEntities.Games.Remove(game);
            _dbUsersEntities.SaveChanges();
        }
        public void AddOrUpdateUser(User user)
        {
            _dbUsersEntities.Users.AddOrUpdate(user);
            _dbUsersEntities.SaveChanges();
        }

        public bool CheckUserByName(User user)
        {
            return _dbUsersEntities.Users.Any(u => u.login == user.login);
        }
        public string GetUserPassByLogin(string login)
        {
            User temp = _dbUsersEntities.Users.First(u => u.login == login);
            return temp?.password;
        }
    }
}
