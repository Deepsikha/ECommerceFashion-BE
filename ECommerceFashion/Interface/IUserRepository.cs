using DataAccessLayer;

namespace ECommerceFashion.Interface
{
    public interface IUserRepository
    {
        Task<List<UserMaster>> GetUsersList();
        Task<UserMaster> GetUserById(int id);
        Task<UserMaster> GetUser(string username, string password);
        Task<bool> AddUserDetails(UserMaster users);
        Task<UserMaster> GetUserByEmail(string email);
    }
}
