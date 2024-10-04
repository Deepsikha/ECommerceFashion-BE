using DataAccessLayer;
using ECommerceFashion.Interface;
using ECommerceFashion.ViewModels;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
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
        public async Task<int> AddUserDetails(UserDetailsVM users)
        {
            try
            {
                if (users == null)
                {
                    return 0;
                }
                var alreadyExist = _userRepository.GetUserByEmail(users.EmailAddress);
                if (alreadyExist != null)
                {
                    return -1;
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
                return userdata ? 1 : 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }
        }
        public static string EncryptPassword(string text)
        {
            var key = Encoding.UTF8.GetBytes("E546C8DF278CD5931069B522E695D4F2");

            using (var aesAlg = Aes.Create())
            {
                using (var encryptor = aesAlg.CreateEncryptor(key, aesAlg.IV))
                {
                    using (var msEncrypt = new MemoryStream())
                    {
                        using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                        using (var swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(text);
                        }

                        var iv = aesAlg.IV;

                        var decryptedContent = msEncrypt.ToArray();

                        var result = new byte[iv.Length + decryptedContent.Length];

                        Buffer.BlockCopy(iv, 0, result, 0, iv.Length);
                        Buffer.BlockCopy(decryptedContent, 0, result, iv.Length, decryptedContent.Length);

                        return Convert.ToBase64String(result);
                    }
                }
            }
        }

        public static string DecryptedPassword(string cipherText)
        {
            var fullCipher = Convert.FromBase64String(cipherText);

            var iv = new byte[16];
            var cipher = new byte[16];

            Buffer.BlockCopy(fullCipher, 0, iv, 0, iv.Length);
            Buffer.BlockCopy(fullCipher, iv.Length, cipher, 0, iv.Length);
            var key = Encoding.UTF8.GetBytes("E546C8DF278CD5931069B522E695D4F2");

            using (var aesAlg = Aes.Create())
            {
                using (var decryptor = aesAlg.CreateDecryptor(key, iv))
                {
                    string result;
                    using (var msDecrypt = new MemoryStream(cipher))
                    {
                        using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {
                            using (var srDecrypt = new StreamReader(csDecrypt))
                            {
                                result = srDecrypt.ReadToEnd();
                            }
                        }
                    }

                    return result;
                }
            }
        }

        public async Task<UserMaster> GetUser(string email, string password)
        {
            var data = await _userRepository.GetUserByEmail(email);

            var decryptedPassword = DecryptedPassword(data.Password);
            if (decryptedPassword == password)
                return data;
            return null;
        }

        public string GenerateToken(UserMaster user)
        {
            
            var claims = new[]
            {
                new Claim("email", user.EmailAddress),
                new Claim("id", user.Id.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Issuer"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
