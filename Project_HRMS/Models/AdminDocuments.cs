using System.ComponentModel.DataAnnotations;

namespace Project_Hrms.Models
{
    public class AdminDocuments
    {
        [Key]
        public int AdminDocId { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Document name is required")]
        public string DocName { get; set; }


        [Required(ErrorMessage = "Document file is required")]
        public string DocFile { get; set; }
    }
}
