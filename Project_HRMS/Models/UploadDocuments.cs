using System.ComponentModel.DataAnnotations;

namespace Project_Hrms.Models
{
    public class UploadDocuments
    {
        [Key]
        public int DocumentId { get; set; }

        [Required]
        public string DocumentName { get; set; }

        [Required]
        public string DocumentType { get; set; }
       
    }
}
