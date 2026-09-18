using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_Hrms.Models
{
    public class PromotionViewModels
    {
        public int PromotionId { get; set; }
        public string? EmployeeName { get; set; }
        public string? ProfilePicture { get; set; }
        public string? PromotionDate { get; set; }

        [Required(ErrorMessage="select employee" )]
        [Display(Name="Employee")]
        public int UserId { get; set; }
        [Required(ErrorMessage = "select current designation")]
        [Display(Name = "DesignationFrom")]
        public string DesignationFrom { get; set; }
        [Required(ErrorMessage = "select promotion designation")]
        [Display(Name = "DesignationTo")]
        public string DesignationTo { get; set; }
        [Required(ErrorMessage = "select promotion date")]
        [Display(Name = "Promotion date")]
        public DateTime Date { get; set; }
        public List<SelectListItem> Users { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> Designations { get; set; } = new List<SelectListItem>();

    }
}
