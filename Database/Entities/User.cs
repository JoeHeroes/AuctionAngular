using System.ComponentModel;

namespace Database.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        [DisplayName("Name")]
        public string Name { get; set; }
        [DisplayName("SureName")]
        public string SureName { get; set; }
        [DisplayName("Date Of Birth")]
        public DateTime? DateOfBirth { get; set; }
        public string Nationality { get; set; }
        public string Phone { get; set; }
        public string PasswordHash { get; set; }
        [DisplayName("Role")]
        public int RoleId { get; set; }
        [DisplayName("Address")]
        public string ProfilePicture { get; set; }
        public bool EmialConfirmed { get; set; }
    }
}
