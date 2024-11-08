namespace EasyApply.Dto
{
    public class ApplicationUserDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string ProfilePicture { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public string TimezoneId { get; set; }
        public bool IsActive { get; set; }
        public DateTimeOffset RegisterDate { get; set; }
    }
}