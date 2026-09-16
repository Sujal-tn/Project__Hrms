using System.ComponentModel.DataAnnotations;

namespace Project_Hrms.Models
{
    public class TrainingType
    {
        [Key]
        public int TrainingTypeId { get; set; }

        [Required]
        public string TrainingTypeName { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Status { get; set; }
    }
}
