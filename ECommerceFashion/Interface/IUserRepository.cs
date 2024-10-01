using DataAccessLayer;
using ECommerceFashion.ViewModels;

namespace ECommerceFashion.Interface
{
    public interface IUserRepository
    {
        Task<List<UserMaster>> GetUsersList();
        Task<UserMaster> GetUserById(int id);
        Task<bool> AddUserDetails(UserMaster users);
        Task<UserMaster> GetUserByEmail(string email);
    }
}
