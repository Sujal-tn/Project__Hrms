using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Project_Hrms.Models
{
    public class TerminationViewModels
    {
        public int TerminationId { get; set; }
        public string? EmployeeName { get; set; }
        public string? ProfilePicture { get; set; }
        [Required(ErrorMessage = "select employee")]
        [Display(Name = "employee")]
        public int UserId { get; set; }
        [Required(ErrorMessage = "select termination type")]
        [Display(Name = "Termination Type")]
        public string TerminationType { get; set; }

        public string? TerminationDate { get; set; }
        [Required(ErrorMessage = "select notice date")]
        [Display(Name = "Notice Date")]
        public DateTime NoticeDate { get; set; }
        [Required(ErrorMessage = "select resign date")]
        [Display(Name = "Resign Date")]
        public DateTime ResignDate { get; set; }
        [Required(ErrorMessage = "fill the reason")]
        [Display(Name = "Reason")]
        public string Reason { get; set; }
        public List<SelectListItem> Users { get; set; } = new List<SelectListItem>();
    }
}
