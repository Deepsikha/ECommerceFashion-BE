using DataAccessLayer;
using ECommerceFashion.Interface;
using ECommerceFashion.ViewModels;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using XSystem.Security.Cryptography;

namespace ECommerceFashion.Services
{
    public class UserService : IUserService
    {
        private IConfiguration _config;
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository, IConfiguration config)
        {
            _userRepository = userRepository;
            _config = config;
        }

        public async Task<List<UserMaster>> GetUserListAsync()
        {
            try
            {
                var data = await _userRepository.GetUsersList();
                return data;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }
        }
        public async Task<UserMaster> GetUserById(int id)
        {
            try
            {
                var data = await _userRepository.GetUserById(id);
                return data;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }
        }
        public async Task<bool> AddUserDetails(UserDetailsVM users)
        {
            try
            {
                if (users == null)
                {
                    return false;
                }
                UserMaster userDetails = new()
                {
                    FirstName = users.FirstName,
                    LastName = users.LastName,
                    EmailAddress = users.EmailAddress,
                    Password = EncryptPassword(users.Password),
                    Address = users.Address,
                    PhoneNumber = users.PhoneNumber,
                    IsTnCApplied = users.IsTnCApplied,
                };

                var userdata = await _userRepository.AddUserDetails(userDetails);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }
        }
        private string EncryptPassword(String password)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] encrypt;
            UTF8Encoding encode = new UTF8Encoding();
            //encrypt the given password string into Encrypted data  
            encrypt = md5.ComputeHash(encode.GetBytes(password));
            StringBuilder encryptdata = new StringBuilder();
            //Create a new string by using the encrypted data  
            for (int i = 0; i < encrypt.Length; i++)
            {
                encryptdata.Append(encrypt[i].ToString());
            }
            return encryptdata.ToString();
        }
        private string GenerateJSONWebToken(UserMaster userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              null,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
