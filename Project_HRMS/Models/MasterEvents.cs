using System.ComponentModel.DataAnnotations;

namespace Project_Hrms.Models
{
    public class MasterEvents
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
      
        public string Color { get; set; }
    }
}
