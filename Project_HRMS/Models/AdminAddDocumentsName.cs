using System.ComponentModel.DataAnnotations;

namespace Project_Hrms.Models
{
    public class AdminAddDocumentsName
    {
        [Key]
        public int id { get; set; }

        [Required (ErrorMessage = "Document name is required")]
        public string ADocumentName { get; set; }

    }
}
