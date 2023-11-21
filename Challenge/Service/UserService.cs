using Challenge.Database;
using Challenge.Entity;

namespace Challenge.Service
{
    public class UserService : IUserService
    {
            private readonly MyDbContext _context;

            public UserService(MyDbContext context)
            {
                _context = context;
            }

            public void AddUser(User user)
            {
                _context.Users.Add(user);
                _context.SaveChanges();
            }

            public void UpdateUser(User user)
            {
                _context.Users.Update(user);
                _context.SaveChanges();
            }

            public User ValidateUser(string email, string password)
            {
                return _context.Users.SingleOrDefault(u => u.Email == email && u.Password == password);
            }
    }
}
