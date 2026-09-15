using System.ComponentModel.DataAnnotations;
<<<<<<< HEAD
using System.ComponentModel.DataAnnotations.Schema;
=======
>>>>>>> main

namespace Project_Hrms.Models
{
    public class Projects
    {
        [Key]
        public int ProjectId { get; set; }

<<<<<<< HEAD
        [Required(ErrorMessage = "Project Name is required.")] 
        [StringLength(255, ErrorMessage = "Project Name cannot exceed 255 characters.")] 
        public string ProjectName { get; set; }

        [Required(ErrorMessage = "Client Name is required.")]
        [StringLength(255, ErrorMessage = "Client Name cannot exceed 255 characters.")]
        public string ClientName { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Start Date is required.")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End Date is required.")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "Priority is required.")]
        [StringLength(50, ErrorMessage = "Priority cannot exceed 50 characters.")]
        public string Priority { get; set; }

        [Required(ErrorMessage = "Project Value is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Project Value must be greater than or equal to 0.")] 
        public double ProjectValue { get; set; }

        [Required(ErrorMessage = "Price Type is required.")]
        [StringLength(50, ErrorMessage = "Price Type cannot exceed 50 characters.")]
        public string PriceType { get; set; }

        [StringLength(255, ErrorMessage = "File Path cannot exceed 255 characters.")]
        public string FilePath { get; set; }

        [StringLength(255, ErrorMessage = "Logo Path cannot exceed 255 characters.")] 
        public string LogoPath { get; set; }

        [Required(ErrorMessage = "Status is required.")]
        [StringLength(50, ErrorMessage = "Status cannot exceed 50 characters.")]
        public string Status { get; set; }

        [Required(ErrorMessage = "Manager Name is required.")]
        public string ManagerName { get; set; }


        public ICollection<Tasks> Tasks { get; set; }

        public virtual ICollection<User> Users { get; set; } = new List<User>();

        public List<Timesheet> Timesheets { get; set; }
=======
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
>>>>>>> main

    }
}
