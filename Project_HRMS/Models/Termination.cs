using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Project_Hrms.Models.EmployeeModel;

namespace Project_Hrms.Models
{
    public class Termination
    {
        [Key]
        public int TerminationId { get; set; }
        [Required(ErrorMessage="select employee")]
        [ForeignKey("User")]
        public int UserId { get; set; }
        [Required(ErrorMessage="select termination type ")]
        public string TerminationType { get; set; }
        [Required(ErrorMessage = "fill the notice date")]
        public DateTime NoticeDate { get; set; }
        [Required(ErrorMessage = "fill the resign date")]
        public DateTime ResignDate { get; set; }
        [Required(ErrorMessage = "fill the reason")]
        [StringLength(100)]
        public string Reason { get; set; }
        public virtual User? User { get; set; }


    }
}
