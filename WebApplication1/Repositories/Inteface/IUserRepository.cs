using OngTDE.BackEnd.Dtos;
using OngTDE.BackEnd.Models;

namespace OngTDE.BackEnd.Repositories.Inteface
{
    public interface IUserRepository
    {
        Task<User> UserAuthenticator(User user);
        Task<User> GetByEmail(string email);
        Task<User> CreateUser(User user);
        Task<User> UpdateUser(User user);
        Task<User> DeleteUser(string email);
        Task<List<UserDto>> GetAllUsers();
        Task<User> GetById(int? id);
        Task<User> DeleteById(int? id);
    }
}
