using Project_Hrms.Models;
using Project_Hrms.Models.EmployeeModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_Hrms.Models
{
    public class Promotion
    {
        [Key]
        public int PromotionId { get; set; }
        [Required]
        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        [Required]
        public string DesignationFrom { get; set; }
        [Required]
        public string DesignationTo { get; set; }
        [Required]
        public DateTime Date { get; set; }
        public virtual User User { get; set; }

    }
}
