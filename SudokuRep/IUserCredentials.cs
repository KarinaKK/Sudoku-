

using SudokuRep;

namespace SudokuWPF
{
    public interface IUserCredentials
    {
        void AddOrUpdateUser(User user);
        bool CheckUserByName(User user);
        string GetUserPassByLogin(string login);
        User GetUserByCredentials(string login, string hashpassword);
    }
}