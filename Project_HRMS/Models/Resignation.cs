using Project_Hrms.Models.EmployeeModel;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_Hrms.Models
{
    public class Resignation
    {
        [Key]
        public int ResignationId { get; set; }
        [Required(ErrorMessage = "select employee")]
        [ForeignKey(nameof(User))]
        public int UserId { get; set; }

        [Required(ErrorMessage = "select department")]
        [ForeignKey(nameof(Department))]
        public int DepartmentId { get; set; }

        [Required(ErrorMessage = "fill the notice date")]
        [DataType(DataType.Date)]
        public DateTime NoticeDate { get; set; }

        [Required(ErrorMessage = "fill the resign date")]
        [DataType(DataType.Date)]
        public DateTime ResignDate { get; set; }

        [Required(ErrorMessage = "fill the reason")]
        [StringLength(500, MinimumLength = 5, ErrorMessage = "reason must be between 5 and 500 charcacters")]
        public string Reason { get; set; }
        public virtual User? User { get; set; }
        public virtual Department? Department { get; set; }


    }
}
