using DataAccessLayer;
using ECommerceFashion.ViewModels;

namespace ECommerceFashion.Interface
{
    public interface IUserService
    {
        Task<List<UserMaster>> GetUserListAsync();
        Task<UserMaster> GetUserById(int id);
        Task<int> AddUserDetails(UserDetailsVM users);
        //Task<UserMaster> GetUserByEmail(string email);
        string GenerateToken(UserMaster user);
        Task<UserMaster> GetUser(string email, string password);
    }
}
