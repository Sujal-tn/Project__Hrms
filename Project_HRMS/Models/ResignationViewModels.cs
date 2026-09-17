using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Project_Hrms.Models
{
    public class ResignationViewModels
    {
        public int ResignationId { get; set; }
        public string? EmployeeName { get; set; }
        public string? ProfilePicture { get; set; }
        public string? DepartmentName { get; set; }
        public string? NoticeDateFormat { get; set; }
        public string? ResignDateFormat { get; set; }
        [Required(ErrorMessage = "select employee")]
        [Display(Name = "Resigning Employee")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "select department.")]
        [Display(Name = "Department")]
        public int DepartmentId { get; set; }

        [Required(ErrorMessage = "fill the notice date")]
        [DataType(DataType.Date)]
        [Display(Name = "Notice Date")]
        public DateTime NoticeDate { get; set; } = DateTime.Today;

        [Required(ErrorMessage = "fill the resign date")]
        [DataType(DataType.Date)]
        [Display(Name = "Resignation Date")]
        public DateTime ResignDate { get; set; } = DateTime.Today.AddDays(30);

        [Required(ErrorMessage = "fill the reason")]
        [StringLength(500, MinimumLength = 5, ErrorMessage = "reason must be between 5 and 500 characters.")]
        public string Reason { get; set; }
        public List<SelectListItem> Users { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> Departments { get; set; } = new List<SelectListItem>();

    }
}