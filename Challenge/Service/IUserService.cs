using Challenge.Entity;

namespace Challenge.Service
{
    public interface IUserService
    {
        void AddUser(User user);

        void UpdateUser(User user);

        User ValidateUser(string email, string password);
    }
}
