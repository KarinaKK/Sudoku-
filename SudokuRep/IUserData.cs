using System.Linq;
using SudokuRep;

namespace SudokuWPF
{
    public interface IUserData
    {
        void AddOrUpdateGame(Game game);
        IQueryable GetRating();

        void DeleteGame(Game game);


    }
}
