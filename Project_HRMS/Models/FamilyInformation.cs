using Project_Hrms.Models.EmployeeModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_Hrms.Models
{
    public class FamilyInformation
    {
        [Key]
        public int familyInfoId { get; set; }

        public string Name { get; set; }
        public string Relation { get; set; }
        public string DateOfBirth { get; set; }
        public string  Phone { get; set; }

        [ForeignKey("UserId")]
        public int UserId {  get; set; }
        public User User { get; set; }
    }
}
