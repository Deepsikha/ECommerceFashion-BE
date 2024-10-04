using DataAccessLayer;
using ECommerceFashion.Data;
using ECommerceFashion.Interface;
using Microsoft.EntityFrameworkCore;

namespace ECommerceFashion.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _dataContext;
        public UserRepository(DataContext dataContext) { _dataContext = dataContext; }

        public async Task<List<UserMaster>> GetUsersList()
        {
            var data = await _dataContext.UserMaster.ToListAsync();
            return data;
        }

        public async Task<UserMaster> GetUserById(int id)
        {
            var data = await _dataContext.UserMaster.FirstOrDefaultAsync(x => x.Id == id);
            return data;
        }

        public async Task<bool> AddUserDetails(UserMaster users)
        {
            var data = await _dataContext.UserMaster.AddAsync(users);
            _dataContext.SaveChanges();
            return true;
        }
        
        public async Task<UserMaster> GetUserByEmail(string email)
        {
            var data = await _dataContext.UserMaster.FirstOrDefaultAsync(x => x.EmailAddress == email);
            return data;
        }

        public async Task<UserMaster> GetUser(string email, string password)
        {
            return await _dataContext.UserMaster.FirstOrDefaultAsync(u => u.EmailAddress== email && u.Password == password);
        }
    }
}
