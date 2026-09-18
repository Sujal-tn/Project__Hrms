using System.ComponentModel.DataAnnotations;

namespace Project_Hrms.Models
{
        public class EmployeeAddDocumentsName
    {
        public int id { get; set; }


        [Required(ErrorMessage = "Document name is required")]
        public string EDocumentName { get; set; }
   
    }
}
