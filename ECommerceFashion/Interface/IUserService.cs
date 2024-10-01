using DataAccessLayer;
using ECommerceFashion.ViewModels;

namespace ECommerceFashion.Interface
{
    public interface IUserService
    {
        Task<List<UserMaster>> GetUserListAsync();
        Task<UserMaster> GetUserById(int id);
        Task<bool> AddUserDetails(UserDetailsVM users);
        //Task<UserMaster> GetUserByEmail(string email);
    }
}
