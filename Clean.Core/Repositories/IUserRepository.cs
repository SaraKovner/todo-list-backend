using Clean.Core.Entities;

namespace Clean.Core.Repositories
{
    public interface IUserRepository
    {
        List<User> GetUsers();
        User? GetById(int id);
        User? GetByUsername(string username);
        User Add(User user);
        User Update(User user);
        void Delete(int id);
    }
}