using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer
{
    public class UserMaster
    {
        [Key]
        public int Id { get; set; }
        public string? FirstName{ get; set; }
        public string? LastName{ get; set; }
        public string? EmailAddress{ get; set; }
        public string? Password{ get; set; }
        public string? Address{ get; set; }
        public string? PhoneNumber{ get; set; }
        public bool IsTnCApplied { get; set; }
    }
}
