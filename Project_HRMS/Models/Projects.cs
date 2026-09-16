using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_Hrms.Models
{
    public class Projects
    {
        [Key]
        public int ProjectId { get; set; }

        public ICollection<Tasks> Tasks { get; set; }

        public virtual ICollection<User> Users { get; set; } = new List<User>();

        public List<Timesheet> Timesheets { get; set; }

        [Required]
        public string ProjectName { get; set; }

        [Required]
        public string ClientName { get; set; }

        [Required]
        public string ProjectDescription { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public string Priority { get; set; }

        [Required]
        public double ProjectValue { get; set; }

        [Required]
        public string PriceType { get; set; }

        public string? FilePath { get; set; }

        public string? LogoPath { get; set; }

        [Required]
        public string Status { get; set; }

        [Required]
        public string? ManagerName { get; set; }

    }
}
