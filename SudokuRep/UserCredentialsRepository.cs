﻿using System.Data.Entity.Migrations;
using System.Linq;
using SudokuWPF;

namespace SudokuRep
{
    public class UserCredentialsRepository:IUserCredentials
    {
        private SudokuUsersEntities _dbUsersEntities;

        public UserCredentialsRepository(SudokuUsersEntities entities)
        {
            _dbUsersEntities = entities;
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
