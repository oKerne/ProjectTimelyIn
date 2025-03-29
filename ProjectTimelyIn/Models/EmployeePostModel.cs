using ProjectTimelyIn.Data;

namespace ProjectTimelyIn.API.Models
{
    public class EmployeePostModel
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
       
        public int RoleId { get; set; }
        public string PasswordHash { get; set; }
    }
}

