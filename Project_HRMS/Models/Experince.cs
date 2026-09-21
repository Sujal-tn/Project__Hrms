using Project_Hrms.Models.EmployeeModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_Hrms.Models
{
    public class Experince
    {
        public  int ExperinceId { get; set; }

        public string Designation { get; set; }

        public string FromDate { get; set; }

        public string ToDate { get; set;  }

        public  string CompanyName { get; set; }

        [ForeignKey("UserId")]
        public int UserId{ get; set; }

        public User User { get; set; }
    }
}
