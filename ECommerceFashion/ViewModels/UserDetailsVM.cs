namespace ECommerceFashion.ViewModels
{
    public class UserDetailsVM
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? EmailAddress { get; set; }
        public string? Password { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public bool IsTnCApplied { get; set; }
    }

    public class TokenVM
    {
        public string? Token { get; set; }
    }
}