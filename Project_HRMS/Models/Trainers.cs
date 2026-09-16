using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_Hrms.Models
{
    public class Trainers
    {
        [Key]
        public int TrainerId { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Role { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        public string Description { get; set; }

        [Required]
        public string Status { get; set; }

        public string? ProfilePicture { get; set; }

        [NotMapped]
        public IFormFile? imagename { get; set; }

        public long Phone { get; set; }
    }
}
