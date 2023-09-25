using TestMVC.Models;
using TestMVC.ViewModel;

namespace TestMVC.Repository
{
    public interface IUserRepository : IRepository<User>
    {
        bool Find(string UserName, string Password);
        User GetUser(string Username);
        List<string> GetRoles(int userId);
        void Insert(RegisterViewModel registerVM);
    }
}
