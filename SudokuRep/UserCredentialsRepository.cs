using System.Data.Entity.Migrations;
using System.Linq;

namespace SudokuRep
{
    public class UserCredentialsRepository
    {
        private SudokuUsersEntities _dbUsersEntities;

        public UserCredentialsRepository()
        {
            _dbUsersEntities =new SudokuUsersEntities();
        }

        public void AddOrUpdateUser(User user)
        {
            _dbUsersEntities.Users.AddOrUpdate(user);
            _dbUsersEntities.SaveChanges();
        }

        public bool CheckUserByName(User user)
        {
            return _dbUsersEntities.Users.FirstOrDefault(u => u.login == user.login)!=null;
        }
        public string GetUserPassByLogin(string login)
        {
            User temp = _dbUsersEntities.Users.FirstOrDefault(u => u.login==login);
            return temp?.password;
        }
        public User GetUserByCredentials(string login,string hashpassword)
        {
            return _dbUsersEntities.Users.FirstOrDefault(u => u.login == login&&u.password==hashpassword);
            
        }
    }
}
