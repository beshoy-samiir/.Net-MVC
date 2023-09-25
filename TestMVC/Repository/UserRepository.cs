using Microsoft.EntityFrameworkCore;
using TestMVC.Models;
using TestMVC.ViewModel;

namespace TestMVC.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        ApplicationDbContext context;
        public UserRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }
        public User GetUser(string Username)
        {
            User user =
               context.Users.FirstOrDefault(u => u.Name == Username);
            return user;
        }

        public bool Find(string UserName, string Password)
        {
            User user = context.Users.FirstOrDefault(u => u.Name == UserName && u.Password == Password);
            if (user != null)
                return true;
            else
                return false;
        }
        public List<string> GetRoles(int userId)
        {
            return context.UserRoles.Include(u => u.Role)
                .Where(u => u.UserID == userId)
                .Select(u => u.Role.Name).ToList();
        }
        public void Insert(RegisterViewModel registerVM)
        {
            context.Add(registerVM);
        }
    }
}
