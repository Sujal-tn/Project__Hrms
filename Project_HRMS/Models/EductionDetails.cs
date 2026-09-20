using Project_Hrms.Models.EmployeeModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_Hrms.Models
{
    public class EductionDetails
    {
        [Key]
        public int EducationId { get; set; }

        public string EducationType { get; set; }

        public string UniversityName { get; set; }

        public string Startdate {  get; set; }

        public string EndDate { get; set; }

        [ForeignKey("UserId")]
        public int UserId { get; set; }

        public User User { get; set; }
    }
}
