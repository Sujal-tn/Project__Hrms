using Project_Hrms.Models.EmployeeModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Project_Hrms.Models.EmployeeModel;


namespace Project_Hrms.Models
{
    public class Trainings
    {
        [Key]
        public int TrainingId { get; set; }

        [ForeignKey("Trainers")]
        public int TrainerId { get; set; }
        public Trainers Trainer { get; set; }


        [ForeignKey("TrainingType")]
        public int TrainingTypeId { get; set; }
        public TrainingType TrainingType { get; set; }


        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal TrainingCost { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Status { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }


        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public string CreatedBy { get; set; }

        [Required]
        public string ModifiedBy { get; set; }

        [Required]
        public DateTime ModifiedAt { get; set; }

        [NotMapped]
        public string? ProfilePicture { get; set; }

        [NotMapped]
        public List<string> images { get; set; }
    }

}