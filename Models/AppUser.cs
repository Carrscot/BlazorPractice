using System.ComponentModel.DataAnnotations;

namespace PracticeApp.Models
{
    public class AppUser
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string PhoneNo { get; set; }

        public string Role { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }
    }
}